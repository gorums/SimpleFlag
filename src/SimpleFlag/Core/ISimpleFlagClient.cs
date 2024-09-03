using SimpleFlag.Core.Models;

namespace SimpleFlag.Core;

/// <summary>
/// This interface defines the contract for the SimpleFlagService.
/// </summary>
public interface ISimpleFlagClient
{
    /// <summary>
    /// Sets the domain for the feature flag.
    /// </summary>
    /// <param name="domain"></param>
    /// <returns></returns>
    ISimpleFlagClient Domain(string domain);

    /// <summary>
    /// Evaluates the feature flag.
    /// </summary>
    /// <param name="flag">The flag</param>
    /// <param name="defaultValue"> the default value</param>
    /// <param name="user">The user</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> GetValueAsync(string flag, bool defaultValue, FeatureFlagUser? user = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Evaluates the feature flag.
    /// </summary>
    /// <param name="flag">The flag</param>
    /// <param name="user">The user</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="SimpleFlagDoesNotExistException">Thrown when the flag does not exist</exception>
    /// <exception cref="SimpleFlagUserDoesNotExistInSegmentException">Thrown when the user does not exist in the segment</exception>
    Task<bool> GetValueAsync(string flag, FeatureFlagUser? user = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Add a feature flag.
    /// </summary>
    /// <param name="featureFlag">The feature flag to add</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<FeatureFlag> AddFeatureFlagAsync(FeatureFlag featureFlag, CancellationToken cancellationToken = default);
}
