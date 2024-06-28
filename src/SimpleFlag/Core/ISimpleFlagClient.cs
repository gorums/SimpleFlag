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
    /// Evaluates the feature flag for boolean.
    /// </summary>
    /// <param name="flag"></param>
    /// <param name="user"></param>
    /// <param name="defaultValue"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool?> EvaluateAsync(string flag, FeatureFlagUser? user = null, bool? defaultValue = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Evaluates the feature flag for integer.
    /// </summary>
    /// <param name="flag"></param>
    /// <param name="user"></param>
    /// <param name="defaultValue"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int?> EvaluateAsync(string flag, FeatureFlagUser? user = null, int? defaultValue = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Evaluates the feature flag for double.
    /// </summary>
    /// <param name="flag"></param>
    /// <param name="user"></param>
    /// <param name="defaultValue"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<double?> EvaluateAsync(string flag, FeatureFlagUser? user = null, double? defaultValue = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Evaluates the feature flag for float.
    /// </summary>
    /// <param name="flag"></param>
    /// <param name="user"></param>
    /// <param name="defaultValue"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<float?> EvaluateAsync(string flag, FeatureFlagUser? user = null, float? defaultValue = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Evaluates the feature flag for string.
    /// </summary>
    /// <param name="flag"></param>
    /// <param name="user"></param>
    /// <param name="defaultValue"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string?> EvaluateAsync(string flag, FeatureFlagUser? user = null, string? defaultValue = null, CancellationToken cancellationToken = default);

}
