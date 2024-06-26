namespace SimpleFlag.Core.Models;
public class FeatureFlag
{
    public Guid Id { get; set; } = Guid.NewGuid();
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

    // evaluate the feature flag
    public FeatureFlagVariant? Evaluate<T>(T user) where T : FeatureFlagUser
    {
        // check if the feature flag is enabled
        if (!Enabled)
        {
            return DefaultOff;
        }

        if (Rules.Count > 0)
        {
            foreach (var rule in Rules)
            {
                if (rule.Evaluate(user) && FeatureFlagRollout.ShouldEvaluateFeature(rule.Value, Name, user.Id.ToString()))
                {
                    return rule.Value;
                }
            }
        }

        if (Variants.Count > 0)
        {
            foreach (var variant in Variants)
            {
                if (FeatureFlagRollout.ShouldEvaluateFeature(variant, Name, user.Id.ToString()))
                {
                    return variant;
                }
            }
        }

        return DefaultOn ?? DefaultOff;
    }
}
