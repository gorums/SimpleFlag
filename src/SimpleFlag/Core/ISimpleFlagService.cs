namespace SimpleFlag.Core;

/// <summary>
/// This interface defines the contract for the SimpleFlagService.
/// </summary>
public interface ISimpleFlagService
{
    /// <summary>
    /// Evaluates the flag and returns the result.
    /// </summary>
    /// <param name="flag">The flag to evaluate</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns></returns>
    Task<bool> EvaluateAsync(string flag, CancellationToken cancellationToken = default);

    /// <summary>
    /// Tries to evaluate the flag and returns the result.
    /// </summary>
    /// <param name="flag">The flag to evaluate</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>success = false if the flag does not exist</returns>
    Task<(bool success, bool result)> TryEvaluateAsync(string flag, CancellationToken cancellationToken = default);
}
