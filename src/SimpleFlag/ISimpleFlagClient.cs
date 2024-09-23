using SimpleFlag.Core.Entities;

namespace SimpleFlag;

/// <summary>
/// This interface defines the contract for the SimpleFlagService.
/// </summary>
public interface ISimpleFlagClient
{
    /// <summary>
    /// Evaluates the feature flag.
    /// </summary>
    /// <param name="domain">The domain</param>
    /// <param name="flag">The flag</param>
    /// <param name="defaultValue"> the default value</param>
    /// <param name="user">The user</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> GetValueAsync(string domain, string flag, bool defaultValue, SimpleFlagUser? user = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Evaluates the feature flag.
    /// </summary>
    /// <param name="flag">The flag</param>
    /// <param name="defaultValue"> the default value</param>
    /// <param name="user">The user</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> GetValueAsync(string flag, bool defaultValue, SimpleFlagUser? user = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Evaluates the feature flag.
    /// </summary>
    /// <param name="domain">The domain</param>
    /// <param name="flag">The flag</param>
    /// <param name="user">The user</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="SimpleFlagDoesNotExistException">Thrown when the flag does not exist</exception>
    /// <exception cref="SimpleFlagUserDoesNotExistInSegmentException">Thrown when the user does not exist in the segment</exception>
    Task<bool> GetValueAsync(string domain, string flag, SimpleFlagUser? user = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Evaluates the feature flag.
    /// </summary>
    /// <param name="flag">The flag</param>
    /// <param name="user">The user</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="SimpleFlagDoesNotExistException">Thrown when the flag does not exist</exception>
    /// <exception cref="SimpleFlagUserDoesNotExistInSegmentException">Thrown when the user does not exist in the segment</exception>
    Task<bool> GetValueAsync(string flag, SimpleFlagUser? user = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all feature flags filter by a domain.
    /// </summary>
    /// <param name="domain">The domain</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<FeatureFlag>> GetFeatureFlagsAsync(string domain, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all feature flags.
    /// </summary>
    /// <param name="domain">The domain</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<FeatureFlag>> GetFeatureFlagsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Add a feature flag with a domain, create the domain if it does not exist.
    /// </summary>
    /// <param name="domain">The domain</param>
    /// <param name="featureFlag">The feature flag to add</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<FeatureFlag> AddFeatureFlagAsync(string domain, FeatureFlag featureFlag, CancellationToken cancellationToken = default);

    /// <summary>
    /// Add a feature flag.
    /// </summary>
    /// <param name="featureFlag">The feature flag to add</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<FeatureFlag> AddFeatureFlagAsync(FeatureFlag featureFlag, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a feature flag.
    /// </summary>
    /// <param name="domain"></param>
    /// <param name="featureFlag"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>

    Task<FeatureFlag> UpdateFeatureFlagAsync(string domain, FeatureFlag featureFlag, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a feature flag.
    /// </summary>
    /// <param name="featureFlag"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>

    Task<FeatureFlag> UpdateFeatureFlagAsync(FeatureFlag featureFlag, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a feature flag.
    /// </summary>
    /// <param name="flagId">The feature flag id</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteFeatureFlagAsync(Guid flagId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all segments.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<FeatureFlagSegment>> GetSegmentsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Add a segment.
    /// </summary>
    /// <param name="segment"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<FeatureFlagSegment> AddSegmentAsync(FeatureFlagSegment segment, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a segment.
    /// </summary>
    /// <param name="segment"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<FeatureFlagSegment> UpdateSegmentAsync(FeatureFlagSegment segment, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a segment.
    /// </summary>
    /// <param name="segmentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteSegmentAsync(Guid segmentId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all users.
    /// </summary>
    /// <param name="segment"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<FeatureFlagUser>> GetUsersAsync(string? segment, CancellationToken cancellationToken = default);

    /// <summary>
    /// Add a list of users.
    /// </summary>
    /// <param name="users"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<FeatureFlagUser>> AddUsersAsync(List<SimpleFlagUser> users, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a list of users.
    /// </summary>
    /// <param name="users"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<SimpleFlagUser>> UpdateUsersAsync(List<SimpleFlagUser> users, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a list of users.
    /// </summary>
    /// <param name="userIds"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteUsersAsync(List<Guid> userIds, CancellationToken cancellationToken = default);

    /// <summary>
    /// Add a segment to a feature flag.
    /// </summary>
    /// <param name="segment"></param>
    /// <param name="flagId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AddSegmentToFeatureFlagAsync(string segment, Guid flagId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Add users to a segment.
    /// </summary>
    /// <param name="users"></param>
    /// <param name="segment"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AddUsersToSegmentAsync(List<SimpleFlagUser> users, string segment, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete users from a segment.
    /// </summary>
    /// <param name="segment"></param>
    /// <param name="userIds"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteUsersFromSegmentAsync(string segment, List<Guid> userIds, CancellationToken cancellationToken = default);

    /// <summary>
    /// Clean users on a segment.
    /// </summary>
    /// <param name="segment"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task CleanUsersOnSegmentAsync(string segment, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all domains.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<FeatureFlagDomain>> GetDomainsAsync(CancellationToken cancellationToken = default);
}
