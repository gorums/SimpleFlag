namespace SimpleFlag;

/// <summary>
/// This interface defines the contract for the SimpleFlagService.
/// </summary>
public interface ISimpleFlagClient
{
    /// <summary>
    /// The service with the feature flags api.
    /// </summary>
    ISimpleFlagService Service { get; }

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
}
