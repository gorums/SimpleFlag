namespace SimpleFlag.Core.Models;
public class FeatureFlag
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public bool Enabled { get; set; } = false;
    public bool Archived { get; set; } = false;
    public FeatureFlagTypes Type { get; set; }
    public List<FeatureFlagVariant> Variants { get; set; } = new List<FeatureFlagVariant>();
    public List<FeatureFlagRule> Rules { get; set; } = new List<FeatureFlagRule>();
    public FeatureFlagVariant? DefaultOff { get; set; }
    public FeatureFlagVariant? DefaultOn { get; set; }
    public FeatureFlagDomain? Domain { get; set; }
}
