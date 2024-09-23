using SimpleFlag.Core.Entities;

namespace SimpleFlag.Core;

/// <summary>
/// This interface defines the contract for the SimpleFlagDataSourceRepository.
/// </summary>
public interface ISimpleFlagDataSourceRepository
{
    /// <summary>
    /// The options for the repository.
    /// </summary>
    SimpleFlagRepositoryOptions SimpleFlagRepositoryOptions { get; set; }

    /// <summary>
    /// Obtains the feature flag.
    /// </summary>
    /// <param name="domain">The domain</param>
    /// <param name="flag">The flag</param>
    /// <param name="user">The user</param>
    /// <param name="cancellation">The cancellation token</param>
    /// <returns>The value of the flag</returns>
    Task<FeatureFlag> GetFeatureFlagAsync(string domain, string flag, FeatureFlagUser? user, CancellationToken cancellation = default);

    /// <summary>
    /// Adds a feature flag.
    /// </summary>
    /// <param name="domain">The domain</param>
    /// <param name="featureFlag">The feature flag to add</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The new feature flag</returns>
    Task<FeatureFlag> AddFeatureFlagAsync(string domain, FeatureFlag featureFlag, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all feature flags filtered by a domain.
    /// </summary>
    /// <param name="domain">The domain</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A collection of feature flags</returns>
    Task<IEnumerable<FeatureFlag>> GetFeatureFlagsAsync(string domain, CancellationToken cancellationToken);

    /// <summary>
    /// Updates a feature flag.
    /// </summary>
    /// <param name="domain">The domain</param>
    /// <param name="featureFlag">The feature flag to update</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The updated feature flag</returns>
    Task<FeatureFlag> UpdateFeatureFlagAsync(string domain, FeatureFlag featureFlag, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a feature flag.
    /// </summary>
    /// <param name="flagId">The feature flag ID</param>
    /// <param name="cancellationToken">The cancellation token</param>
    Task DeleteFeatureFlagAsync(Guid flagId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets all segments.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A collection of segments</returns>
    Task<IEnumerable<FeatureFlagSegment>> GetSegmentsAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Adds a segment.
    /// </summary>
    /// <param name="segment">The segment to add</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The added segment</returns>
    Task<FeatureFlagSegment> AddSegmentAsync(FeatureFlagSegment segment, CancellationToken cancellationToken);

    /// <summary>
    /// Updates a segment.
    /// </summary>
    /// <param name="segment">The segment to update</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The updated segment</returns>
    Task<FeatureFlagSegment> UpdateSegmentAsync(FeatureFlagSegment segment, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a segment.
    /// </summary>
    /// <param name="segmentId">The segment ID</param>
    /// <param name="cancellationToken">The cancellation token</param>
    Task DeleteSegmentAsync(Guid segmentId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets all users in a segment.
    /// </summary>
    /// <param name="segment">The segment</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A collection of users</returns>
    Task<IEnumerable<FeatureFlagUser>> GetUsersAsync(string? segment, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a list of users.
    /// </summary>
    /// <param name="users">The users to add</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The added users</returns>
    Task<IEnumerable<FeatureFlagUser>> AddUsersAsync(List<SimpleFlagUser> users, CancellationToken cancellationToken);

    /// <summary>
    /// Updates a list of users.
    /// </summary>
    /// <param name="users">The users to update</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The updated users</returns>
    Task<IEnumerable<SimpleFlagUser>> UpdateUsersAsync(List<SimpleFlagUser> users, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a list of users.
    /// </summary>
    /// <param name="userIds">The user IDs</param>
    /// <param name="cancellationToken">The cancellation token</param>
    Task DeleteUsersAsync(List<Guid> userIds, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a segment to a feature flag.
    /// </summary>
    /// <param name="segment">The segment</param>
    /// <param name="flagId">The feature flag ID</param>
    /// <param name="cancellationToken">The cancellation token</param>
    Task AddSegmentToFeatureFlagAsync(string segment, Guid flagId, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes users from a segment.
    /// </summary>
    /// <param name="segment">The segment</param>
    /// <param name="userIds">The user IDs</param>
    /// <param name="cancellationToken">The cancellation token</param>
    Task DeleteUsersFromSegmentAsync(string segment, List<Guid> userIds, CancellationToken cancellationToken);

    /// <summary>
    /// Adds users to a segment.
    /// </summary>
    /// <param name="users">The users</param>
    /// <param name="segment">The segment</param>
    /// <param name="cancellationToken">The cancellation token</param>
    Task AddUsersToSegmentAsync(List<SimpleFlagUser> users, string segment, CancellationToken cancellationToken);

    /// <summary>
    /// Cleans users on a segment.
    /// </summary>
    /// <param name="segment">The segment</param>
    /// <param name="cancellationToken">The cancellation token</param>
    Task CleanUsersOnSegmentAsync(string segment, CancellationToken cancellationToken);

    /// <summary>
    /// Gets all domains.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A collection of domains</returns>
    Task<IEnumerable<FeatureFlagDomain>> GetDomainsAsync(CancellationToken cancellationToken);
}

