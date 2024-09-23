using SimpleFlag.Core.Entities;

namespace SimpleFlag.Core.Internal;

/// <summary>
/// This class is the implementation of the ISimpleFlagDataSource.
/// </summary>
internal class SimpleFlagDataSource
{
    private ISimpleFlagDataSourceMigration _dataSourceMigration;
    private ISimpleFlagDataSourceRepository _dataSourceRepository;

    /// <summary>
    /// Initializes a new instance of the SimpleFlagDataSource.
    /// </summary>
    /// <param name="simpleFlagDataSourceOptions"></param>
    /// <exception cref="ArgumentNullException"></exception>
    internal SimpleFlagDataSource(SimpleFlagDataSourceOptions simpleFlagDataSourceOptions)
    {
        _dataSourceMigration = simpleFlagDataSourceOptions.DataSourceMigration ?? throw new ArgumentNullException(nameof(simpleFlagDataSourceOptions.DataSourceMigration));
        _dataSourceRepository = simpleFlagDataSourceOptions.DataSourceRepository ?? throw new ArgumentNullException(nameof(simpleFlagDataSourceOptions.DataSourceRepository));

        _dataSourceMigration.SimpleFlagMigrationOptions = new SimpleFlagMigrationOptions
        {
            ConnectionString = simpleFlagDataSourceOptions.ConnectionString,
            SchemaName = simpleFlagDataSourceOptions.SchemaName,
            TablePrefix = simpleFlagDataSourceOptions.TablePrefix
        };

        _dataSourceRepository.SimpleFlagRepositoryOptions = new SimpleFlagRepositoryOptions
        {
            ConnectionString = simpleFlagDataSourceOptions.ConnectionString,
            SchemaName = simpleFlagDataSourceOptions.SchemaName,
            TablePrefix = simpleFlagDataSourceOptions.TablePrefix
        };
    }

    /// <summary>
    /// Run the migration.
    /// </summary>
    internal void RunMigration() => _dataSourceMigration.Run();

    /// <summary>
    /// Obtain the feature flag if exist.
    /// </summary>
    /// <param name="domain">The domain</param>
    /// <param name="flag">The flag</param>
    /// <param name="user">The user</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>If the flag is enabled</returns>
    /// <exception cref="SimpleFlagDoesNotExistException">Thrown when the flag does not exist</exception></exception>
    internal async Task<FeatureFlag> GetFeatureFlagAsync(string domain, string flag, SimpleFlagUser? user, CancellationToken cancellationToken)
    {
        FeatureFlagUser? featureFlagUser = user is not null ? new FeatureFlagUser(user.Name)
        {
            Attributes = new Dictionary<string, string>(user.Attributes)
        } : default;

        return await _dataSourceRepository.GetFeatureFlagAsync(domain, flag, featureFlagUser, cancellationToken) ?? throw new SimpleFlagDoesNotExistException(flag);
    }

    /// <summary>
    /// Get all feature flags filter by a domain.
    /// </summary>
    /// <param name="domain"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    internal async Task<IEnumerable<FeatureFlag>> GetFeatureFlagsAsync(string domain, CancellationToken cancellationToken)
    {
        return await _dataSourceRepository.GetFeatureFlagsAsync(domain, cancellationToken);
    }

    /// <summary>
    /// Add a feature flag.
    /// </summary>
    /// <param name="domain">The domain</param>
    /// <param name="featureFlag">The feature flag to add</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The feature flag added</returns>
    internal async Task<FeatureFlag> AddFeatureFlagAsync(string domain, FeatureFlag featureFlag, CancellationToken cancellationToken)
    {
        return await _dataSourceRepository.AddFeatureFlagAsync(domain, featureFlag, cancellationToken);
    }

    /// <summary>
    /// Update a feature flag.
    /// </summary>
    /// <param name="domain">The domain</param>
    /// <param name="featureFlag">The feature flag to update</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The feature flag updated</returns>
    internal async Task<FeatureFlag> UpdateFeatureFlagAsync(string domain, FeatureFlag featureFlag, CancellationToken cancellationToken)
    {
        return await _dataSourceRepository.UpdateFeatureFlagAsync(domain, featureFlag, cancellationToken);
    }

    /// <summary>
    /// Delete a feature flag.
    /// </summary>
    /// <param name="flagId">The feature flag id</param>
    /// <param name="cancellationToken">The cancellation token</param>
    internal async Task DeleteFeatureFlagAsync(Guid flagId, CancellationToken cancellationToken)
    {
        await _dataSourceRepository.DeleteFeatureFlagAsync(flagId, cancellationToken);
    }

    /// <summary>
    /// Get all segments.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The segments</returns>
    internal async Task<IEnumerable<FeatureFlagSegment>> GetSegmentsAsync(CancellationToken cancellationToken)
    {
        return await _dataSourceRepository.GetSegmentsAsync(cancellationToken);
    }

    /// <summary>
    /// Add a segment.
    /// </summary>
    /// <param name="segment">The segment to add</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The segment added</returns>
    internal async Task<FeatureFlagSegment> AddSegmentAsync(FeatureFlagSegment segment, CancellationToken cancellationToken)
    {
        return await _dataSourceRepository.AddSegmentAsync(segment, cancellationToken);
    }

    /// <summary>
    /// Update a segment.
    /// </summary>
    /// <param name="segment">The segment to update</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The segment updated</returns>
    internal async Task<FeatureFlagSegment> UpdateSegmentAsync(FeatureFlagSegment segment, CancellationToken cancellationToken)
    {
        return await _dataSourceRepository.UpdateSegmentAsync(segment, cancellationToken);
    }

    /// <summary>
    /// Delete a segment.
    /// </summary>
    /// <param name="segmentId">The segment id</param>
    /// <param name="cancellationToken">The cancellation token</param>
    internal async Task DeleteSegmentAsync(Guid segmentId, CancellationToken cancellationToken)
    {
        await _dataSourceRepository.DeleteSegmentAsync(segmentId, cancellationToken);
    }

    /// <summary>
    /// Get all users.
    /// </summary>
    /// <param name="segment">The segment</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The users</returns>
    internal async Task<IEnumerable<FeatureFlagUser>> GetUsersAsync(string? segment, CancellationToken cancellationToken)
    {
        return await _dataSourceRepository.GetUsersAsync(segment, cancellationToken);
    }

    /// <summary>
    /// Add a list of users.
    /// </summary>
    /// <param name="users">The users to add</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The users added</returns>
    internal async Task<IEnumerable<FeatureFlagUser>> AddUsersAsync(List<SimpleFlagUser> users, CancellationToken cancellationToken)
    {
        return await _dataSourceRepository.AddUsersAsync(users, cancellationToken);
    }

    /// <summary>
    /// Update a list of users.
    /// </summary>
    /// <param name="users">The users to update</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The users updated</returns>
    internal async Task<IEnumerable<SimpleFlagUser>> UpdateUsersAsync(List<SimpleFlagUser> users, CancellationToken cancellationToken)
    {
        return await _dataSourceRepository.UpdateUsersAsync(users, cancellationToken);
    }

    /// <summary>
    /// Delete a list of users.
    /// </summary>
    /// <param name="userIds">The user ids</param>
    /// <param name="cancellationToken">The cancellation token</param>
    internal async Task DeleteUsersAsync(List<Guid> userIds, CancellationToken cancellationToken)
    {
        await _dataSourceRepository.DeleteUsersAsync(userIds, cancellationToken);
    }

    /// <summary>
    /// Add a segment to a feature flag.
    /// </summary>
    /// <param name="segment">The segment</param>
    /// <param name="flagId">The feature flag id</param>
    /// <param name="cancellationToken">The cancellation token</param>
    internal async Task AddSegmentToFeatureFlagAsync(string segment, Guid flagId, CancellationToken cancellationToken)
    {
        await _dataSourceRepository.AddSegmentToFeatureFlagAsync(segment, flagId, cancellationToken);
    }

    /// <summary>
    /// Add users to a segment.
    /// </summary>
    /// <param name="users">The users</param>
    /// <param name="segment">The segment</param>
    /// <param name="cancellationToken">The cancellation token</param>
    internal async Task AddUsersToSegmentAsync(List<SimpleFlagUser> users, string segment, CancellationToken cancellationToken)
    {
        await _dataSourceRepository.AddUsersToSegmentAsync(users, segment, cancellationToken);
    }

    /// <summary>
    /// Delete users from a segment.
    /// </summary>
    /// <param name="segment">The segment</param>
    /// <param name="userIds">The user ids</param>
    /// <param name="cancellationToken">The cancellation token</param>
    internal async Task DeleteUsersFromSegmentAsync(string segment, List<Guid> userIds, CancellationToken cancellationToken)
    {
        await _dataSourceRepository.DeleteUsersFromSegmentAsync(segment, userIds, cancellationToken);
    }

    /// <summary>
    /// Clean users on a segment.
    /// </summary>
    /// <param name="segment">The segment</param>
    /// <param name="cancellationToken">The cancellation token</param>
    internal async Task CleanUsersOnSegmentAsync(string segment, CancellationToken cancellationToken)
    {
        await _dataSourceRepository.CleanUsersOnSegmentAsync(segment, cancellationToken);
    }

    /// <summary>
    /// Get all domains.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The domains</returns>
    internal async Task<IEnumerable<FeatureFlagDomain>> GetDomainsAsync(CancellationToken cancellationToken)
    {
        return await _dataSourceRepository.GetDomainsAsync(cancellationToken);
    }
}
