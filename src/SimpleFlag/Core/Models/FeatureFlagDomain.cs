namespace SimpleFlag.Core.Models;

/// <summary>
/// To organize the feature flag per domain.
/// </summary>
public class FeatureFlagDomain
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;

    public List<FeatureFlagVariant> Flags { get; set; } = new List<FeatureFlagVariant>();
}
