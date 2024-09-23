using Npgsql;
using SimpleFlag.Core;
using SimpleFlag.Core.Entities;
using SimpleFlag.PostgreSQL.Migrations.Metadata;

namespace SimpleFlag.PostgreSQL;

/// <summary>
/// This class represents the PostgreSQL data source repository.
/// </summary>
internal class PostgresDataSourceRepository : ISimpleFlagDataSourceRepository
{
    private static readonly Lazy<ISimpleFlagDataSourceRepository> _dataSourceRepository = new Lazy<ISimpleFlagDataSourceRepository>(() => new PostgresDataSourceRepository());

    /// <summary>
    /// The options for the repository.
    /// </summary>
    public SimpleFlagRepositoryOptions SimpleFlagRepositoryOptions { get; set; } = new SimpleFlagRepositoryOptions();

    /// <summary>
    /// Private constructor to prevent instantiation.
    /// </summary>
    private PostgresDataSourceRepository()
    {
    }

    /// <inheritdoc />
    public static ISimpleFlagDataSourceRepository Instance => _dataSourceRepository.Value;

    /// <inheritdoc />
    public async Task<FeatureFlag> GetFeatureFlagAsync(string domain, string flagKey, FeatureFlagUser? user, CancellationToken cancellation = default)
    {
        FeatureFlag? featureFlag = null;

        using var connection = new NpgsqlConnection(SimpleFlagRepositoryOptions.ConnectionString);

        await connection.OpenAsync(cancellation);

        string query;

        if (!string.IsNullOrEmpty(domain))
        {
            // Query when domain is provided            
            query = @$"SELECT ff.""Id"", ff.""Enabled""
            FROM {CustomMigrationMetaData.SchemaName}.{CustomMigrationMetaData.TablePrefix}_feature_flags ff
            JOIN {CustomMigrationMetaData.SchemaName}.{CustomMigrationMetaData.TablePrefix}_feature_flag_domains ffd ON ff.""DomainId"" = ffd.""Id""
            WHERE ff.""Key"" = @Key AND ffd.""Name"" = @Domain";
        }
        else
        {
            // Query when domain is not provided
            query = @$"SELECT ff.""Id"", ff.""Enabled""
            FROM {CustomMigrationMetaData.SchemaName}.{CustomMigrationMetaData.TablePrefix}_feature_flags ff
            WHERE ff.""Key"" = @Key";
        }

        using (var command = new NpgsqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Key", flagKey);
            if (!string.IsNullOrEmpty(domain))
            {
                command.Parameters.AddWithValue("@Domain", domain);
            }

            using (var reader = await command.ExecuteReaderAsync(cancellation))
            {
                if (await reader.ReadAsync(cancellation))
                {
                    featureFlag = new FeatureFlag
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        Enabled = reader.GetBoolean(reader.GetOrdinal("Enabled"))
                    };
                }
            }
        }

        if (featureFlag is null)
        {
            throw new SimpleFlagDoesNotExistException($"The flag \"{flagKey}\" does not exist.");
        }

        // If a user is provided, check if the user is part of any segment of the flag
        if (user is not null)
        {
            await CheckUserSegmentMembershipAsync(flagKey, user, featureFlag, connection, cancellation);
        }

        return featureFlag;
    }

    /// <inheritdoc />
    public async Task<FeatureFlag> AddFeatureFlagAsync(string domain, FeatureFlag featureFlag, CancellationToken cancellationToken = default)
    {
        using var connection = new NpgsqlConnection(SimpleFlagRepositoryOptions.ConnectionString);
        await connection.OpenAsync(cancellationToken);

        // Check if the name and the domain exist in the database
        string checkQuery = @$"
        SELECT COUNT(*)
        FROM {CustomMigrationMetaData.SchemaName}.{CustomMigrationMetaData.TablePrefix}_feature_flags ff
        LEFT JOIN {CustomMigrationMetaData.SchemaName}.{CustomMigrationMetaData.TablePrefix}_feature_flag_domains ffd ON ff.""DomainId"" = ffd.""Id""
        WHERE ff.""Name"" = @Name AND (ffd.""Name"" = @Domain OR @Domain IS NULL)";

        using (var checkCommand = new NpgsqlCommand(checkQuery, connection))
        {
            checkCommand.Parameters.AddWithValue("@Name", featureFlag.Name);
            checkCommand.Parameters.AddWithValue("@Domain", (object?)domain ?? DBNull.Value);

            var exists = (long)await checkCommand.ExecuteScalarAsync(cancellationToken) > 0;

            if (exists)
            {
                throw new SimpleFlagExistException("A feature flag with the same name and domain already exists.");
            }
        }

        // Check if the domain exists, if not create it
        Guid? domainId = null;
        if (!string.IsNullOrEmpty(domain))
        {
            string domainQuery = @$"
            SELECT ""Id""
            FROM {CustomMigrationMetaData.SchemaName}.{CustomMigrationMetaData.TablePrefix}_feature_flag_domains
            WHERE ""Name"" = @Domain";

            using (var domainCommand = new NpgsqlCommand(domainQuery, connection))
            {
                domainCommand.Parameters.AddWithValue("@Domain", domain);

                var result = await domainCommand.ExecuteScalarAsync(cancellationToken);
                if (result != null)
                {
                    domainId = (Guid)result;
                }
                else
                {
                    // Domain does not exist, create it
                    domainId = Guid.NewGuid();
                    string insertDomainQuery = @$"
                    INSERT INTO {CustomMigrationMetaData.SchemaName}.{CustomMigrationMetaData.TablePrefix}_feature_flag_domains
                    (""Id"", ""Name"")
                    VALUES (@Id, @Name)";

                    using (var insertDomainCommand = new NpgsqlCommand(insertDomainQuery, connection))
                    {
                        insertDomainCommand.Parameters.AddWithValue("@Id", domainId);
                        insertDomainCommand.Parameters.AddWithValue("@Name", domain);

                        await insertDomainCommand.ExecuteNonQueryAsync(cancellationToken);
                    }
                }
            }
        }

        // Insert the new feature flag
        string insertQuery = @$"
        INSERT INTO {CustomMigrationMetaData.SchemaName}.{CustomMigrationMetaData.TablePrefix}_feature_flags 
        (""Id"", ""Name"", ""Description"", ""Key"", ""Enabled"", ""Archived"", ""DomainId"") 
        VALUES (@Id, @Name, @Description, @Key, @Enabled, @Archived, @DomainId)";

        using (var insertCommand = new NpgsqlCommand(insertQuery, connection))
        {
            insertCommand.Parameters.AddWithValue("@Id", featureFlag.Id);
            insertCommand.Parameters.AddWithValue("@Name", featureFlag.Name);
            insertCommand.Parameters.AddWithValue("@Description", featureFlag.Description);
            insertCommand.Parameters.AddWithValue("@Key", featureFlag.Key);
            insertCommand.Parameters.AddWithValue("@Enabled", featureFlag.Enabled);
            insertCommand.Parameters.AddWithValue("@Archived", featureFlag.Archived);
            insertCommand.Parameters.AddWithValue("@DomainId", (object?)domainId ?? DBNull.Value);

            await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        return featureFlag;
    }

    /// <summary>
    /// Checks if the user is part of any segment of the feature flag.
    /// </summary>
    /// <param name="flagKey">The key of the feature flag.</param>
    /// <param name="user">The user to check for segment membership.</param>
    /// <param name="featureFlag">The feature flag object.</param>
    /// <param name="connection">The PostgreSQL connection.</param>
    /// <param name="cancellation">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="SimpleFlagUserDoesNotExistInSegmentException">Thrown if the user does not exist in any segment of the feature flag.</exception>
    private static async Task CheckUserSegmentMembershipAsync(string flagKey, FeatureFlagUser? user, FeatureFlag featureFlag, NpgsqlConnection connection, CancellationToken cancellation)
    {
        // Query to check if the user is part of any segment of the feature flag
        var segmentCheckQuery = @$"
            SELECT EXISTS (
                SELECT 1
                FROM {CustomMigrationMetaData.SchemaName}.{CustomMigrationMetaData.TablePrefix}_feature_flag_segments s
                JOIN {CustomMigrationMetaData.SchemaName}.{CustomMigrationMetaData.TablePrefix}_feature_flag_segment_flags sf ON s.""Id"" = sf.""SegmentId""
                JOIN {CustomMigrationMetaData.SchemaName}.{CustomMigrationMetaData.TablePrefix}_feature_flag_segment_users su ON s.""Id"" = sf.""SegmentId"" 
                JOIN {CustomMigrationMetaData.SchemaName}.{CustomMigrationMetaData.TablePrefix}_feature_flag_users u ON su.""UserId"" = u.""Id""
                WHERE sf.""FlagId"" = @FeatureFlagId AND u.""Name"" = @UserName
            )";

        using (var command = new NpgsqlCommand(segmentCheckQuery, connection))
        {
            command.Parameters.AddWithValue("@FeatureFlagId", featureFlag.Id);
            command.Parameters.AddWithValue("@UserName", user.Name);

            // Execute the query to check for segment membership by user name
            bool userIsInSegment = (bool)await command.ExecuteScalarAsync(cancellation);

            // If the user is part of a segment, you might want to update the FeatureFlag object accordingly
            // For example, you could fetch and add the relevant segments to the FeatureFlag.Segments list
            // This step depends on how you want to use the information about segment membership
            if (!userIsInSegment)
            {
                throw new SimpleFlagUserDoesNotExistInSegmentException($"The flag \"{flagKey}\" does not exist.");
            }
        }
    }

    public Task<IEnumerable<FeatureFlag>> GetFeatureFlagsAsync(string domain, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<FeatureFlag> UpdateFeatureFlagAsync(string domain, FeatureFlag featureFlag, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteFeatureFlagAsync(Guid flagId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FeatureFlagSegment>> GetSegmentsAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<FeatureFlagSegment> AddSegmentAsync(FeatureFlagSegment segment, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<FeatureFlagSegment> UpdateSegmentAsync(FeatureFlagSegment segment, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteSegmentAsync(Guid segmentId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FeatureFlagUser>> GetUsersAsync(string? segment, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FeatureFlagUser>> AddUsersAsync(List<SimpleFlagUser> users, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<SimpleFlagUser>> UpdateUsersAsync(List<SimpleFlagUser> users, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUsersAsync(List<Guid> userIds, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AddSegmentToFeatureFlagAsync(string segment, Guid flagId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUsersFromSegmentAsync(string segment, List<Guid> userIds, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AddUsersToSegmentAsync(List<SimpleFlagUser> users, string segment, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task CleanUsersOnSegmentAsync(string segment, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FeatureFlagDomain>> GetDomainsAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
