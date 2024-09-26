using SimpleFlag.Core.Entities;

namespace SimpleFlag;

/// <summary>
/// This interface defines the contract for the FeatureFlagService.
/// </summary>
public interface ISimpleFlagService
{
    /// <summary>
    /// Get all feature flags filtered by a domain.
    /// </summary>
    /// <param name="domain">The domain</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of feature flags</returns>
    Task<IEnumerable<FeatureFlag>> GetFeatureFlagsAsync(string domain, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all feature flags.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of feature flags</returns>
    Task<IEnumerable<FeatureFlag>> GetFeatureFlagsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Add a feature flag with a domain, create the domain if it does not exist.
    /// </summary>
    /// <param name="domain">The domain</param>
    /// <param name="featureFlag">The feature flag to add</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The added feature flag</returns>
    Task<FeatureFlag> AddFeatureFlagAsync(string domain, FeatureFlag featureFlag, CancellationToken cancellationToken = default);

    /// <summary>
    /// Add a feature flag.
    /// </summary>
    /// <param name="featureFlag">The feature flag to add</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The added feature flag</returns>
    Task<FeatureFlag> AddFeatureFlagAsync(FeatureFlag featureFlag, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a feature flag.
    /// </summary>
    /// <param name="domain">The domain</param>
    /// <param name="featureFlag">The feature flag to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated feature flag</returns>
    Task<FeatureFlag> UpdateFeatureFlagAsync(string domain, FeatureFlag featureFlag, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a feature flag.
    /// </summary>
    /// <param name="featureFlag">The feature flag to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated feature flag</returns>
    Task<FeatureFlag> UpdateFeatureFlagAsync(FeatureFlag featureFlag, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a feature flag.
    /// </summary>
    /// <param name="flagId">The feature flag id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A task representing the asynchronous operation</returns>
    Task DeleteFeatureFlagAsync(Guid flagId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all segments.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of feature flag segments</returns>
    Task<IEnumerable<FeatureFlagSegment>> GetSegmentsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Add a segment.
    /// </summary>
    /// <param name="segment">The segment to add</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The added segment</returns>
    Task<FeatureFlagSegment> AddSegmentAsync(FeatureFlagSegment segment, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a segment.
    /// </summary>
    /// <param name="segment">The segment to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated segment</returns>
    Task<FeatureFlagSegment> UpdateSegmentAsync(FeatureFlagSegment segment, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a segment.
    /// </summary>
    /// <param name="segmentId">The segment id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A task representing the asynchronous operation</returns>
    Task DeleteSegmentAsync(Guid segmentId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all users.
    /// </summary>
    /// <param name="segment">The segment</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of feature flag users</returns>
    Task<IEnumerable<FeatureFlagUser>> GetUsersAsync(string? segment, CancellationToken cancellationToken = default);

    /// <summary>
    /// Add a list of users.
    /// </summary>
    /// <param name="users">The users to add</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of added users</returns>
    Task<IEnumerable<FeatureFlagUser>> AddUsersAsync(List<SimpleFlagUser> users, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a list of users.
    /// </summary>
    /// <param name="users">The users to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of updated users</returns>
    Task<IEnumerable<SimpleFlagUser>> UpdateUsersAsync(List<SimpleFlagUser> users, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a list of users.
    /// </summary>
    /// <param name="userIds">The user ids</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A task representing the asynchronous operation</returns>
    Task DeleteUsersAsync(List<Guid> userIds, CancellationToken cancellationToken = default);

    /// <summary>
    /// Add a segment to a feature flag.
    /// </summary>
    /// <param name="segment">The segment</param>
    /// <param name="flagId">The feature flag id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A task representing the asynchronous operation</returns>
    Task AddSegmentToFeatureFlagAsync(string segment, Guid flagId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Add users to a segment.
    /// </summary>
    /// <param name="users">The users to add</param>
    /// <param name="segment">The segment</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A task representing the asynchronous operation</returns>
    Task AddUsersToSegmentAsync(List<SimpleFlagUser> users, string segment, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete users from a segment.
    /// </summary>
    /// <param name="segment">The segment</param>
    /// <param name="userIds">The user ids</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A task representing the asynchronous operation</returns>
    Task DeleteUsersFromSegmentAsync(string segment, List<Guid> userIds, CancellationToken cancellationToken = default);

    /// <summary>
    /// Clean users on a segment.
    /// </summary>
    /// <param name="segment">The segment</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A task representing the asynchronous operation</returns>
    Task CleanUsersOnSegmentAsync(string segment, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all domains.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of feature flag domains</returns>
    Task<IEnumerable<FeatureFlagDomain>> GetDomainsAsync(CancellationToken cancellationToken = default);
}