namespace SimpleFlag.Core.Models;
public class FeatureFlagVariant
{
    public FeatureFlagTypes Type { get; }

    public string Value { get; }

    public FeatureFlagVariant(FeatureFlagTypes type, string value)
    {
        Type = type;
        Value = value;
    }
}
