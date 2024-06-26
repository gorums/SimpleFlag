namespace SimpleFlag.Core.Models;

/// <summary>
/// Represents the user of the feature flag.
/// </summary>
public interface FeatureFlagUser
{
    Guid Id { get; }

    string Name { get; }
}
