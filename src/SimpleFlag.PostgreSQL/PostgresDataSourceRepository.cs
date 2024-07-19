using Npgsql;
using SimpleFlag.Core;
using SimpleFlag.Core.DataSource;
using SimpleFlag.Core.Models;
using SimpleFlag.PostgreSQL.Migrations.Metadata;

namespace SimpleFlag.PostgreSQL;

/// <summary>
/// This class represents the PostgreSQL data source repository.
/// </summary>
internal class PostgresDataSourceRepository : ISimpleFlagDataSourceRepository
{
    private static readonly Lazy<ISimpleFlagDataSourceRepository> _dataSourceRepository = new Lazy<ISimpleFlagDataSourceRepository>(() => new PostgresDataSourceRepository());

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
        FeatureFlag featureFlag = null;

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

        // If a user is provided, check if the user is part of any segment of the flag
        if (featureFlag is not null && user is not null)
        {
            var segmentCheckQuery = @$"
            SELECT EXISTS (
                SELECT 1
                FROM {CustomMigrationMetaData.TablePrefix}_feature_flag_segments s
                JOIN {CustomMigrationMetaData.TablePrefix}_feature_flag_segment_flags sf ON s.Id = sf.SegmentId 
                JOIN {CustomMigrationMetaData.TablePrefix}_feature_flag_segment_users su ON s.Id = sf.SegmentId 
                JOIN {CustomMigrationMetaData.TablePrefix}_feature_flag_users u ON su.UserId = u.Id
                WHERE sf.FeatureFlagId = @FeatureFlagId AND u.Name = @UserName
            )";

            using (var command = new NpgsqlCommand(segmentCheckQuery, connection))
            {
                command.Parameters.AddWithValue("@FeatureFlagId", featureFlag.Id);
                command.Parameters.AddWithValue("@UserName", user.Name);

                // Execute the query to check for segment membership by user name
                var userIsInSegment = (bool)await command.ExecuteScalarAsync(cancellation);

                // If the user is part of a segment, you might want to update the FeatureFlag object accordingly
                // For example, you could fetch and add the relevant segments to the FeatureFlag.Segments list
                // This step depends on how you want to use the information about segment membership
                if (!userIsInSegment)
                {
                    throw new SimpleFlagUserDoesNotExistInSegmentException($"The flag \"{flagKey}\" does not exist.");
                }
            }
        }

        if (featureFlag is null)
        {
            throw new SimpleFlagDoesNotExistException($"The flag \"{flagKey}\" does not exist.");
        }

        return featureFlag;
    }
}
