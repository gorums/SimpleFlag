using SimpleFlag.Core.Models;

namespace SimpleFlag.Core;

/// <summary>
/// This interface defines the contract for the SimpleFlagService.
/// </summary>
public interface ISimpleFlagClient
{
    ISimpleFlagClient Domain(string domain);

    Task<bool?> EvaluateAsync(string flag, FeatureFlagUser? user = null, bool? defaultValue = null, CancellationToken cancellationToken = default);

    Task<int?> EvaluateAsync(string flag, FeatureFlagUser? user = null, int? defaultValue = null, CancellationToken cancellationToken = default);

    Task<double?> EvaluateAsync(string flag, FeatureFlagUser? user = null, double? defaultValue = null, CancellationToken cancellationToken = default);

    Task<string?> EvaluateAsync(string flag, FeatureFlagUser? user = null, string? defaultValue = null, CancellationToken cancellationToken = default);

}
