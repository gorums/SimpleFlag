namespace SimpleFlag.Core.Models;
public class FeatureFlagVariant
{
    public FeatureFlagTypes Type { get; }

    public string Value { get; }

    public int RolloutPercentage { get; }

    public FeatureFlagVariant(FeatureFlagTypes type, string value) : this(type, value, 100)
    {
    }

    public FeatureFlagVariant(FeatureFlagTypes type, string value, int rolloutPercentage)
    {
        Type = type;
        Value = value;
        RolloutPercentage = rolloutPercentage;
    }
}
