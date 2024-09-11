namespace SimpleFlag.Core.Entities;

/// <summary>
/// To organize the feature flag per domain.
/// </summary>
public class FeatureFlagDomain
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public List<FeatureFlag> FeatureFlags { get; set; } = new List<FeatureFlag>();
}
