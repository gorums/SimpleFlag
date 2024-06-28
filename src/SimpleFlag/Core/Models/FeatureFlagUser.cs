namespace SimpleFlag.Core.Models;

/// <summary>
/// Represents the user of the feature flag.
/// </summary>
public class FeatureFlagUser
{
    public Guid Id { get; }

    public string Name { get; }

    public Dictionary<string, string> Attributes { get; } = new Dictionary<string, string>();

    public FeatureFlagUser(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public static FeatureFlagUser Create(string name)
    {
        return new FeatureFlagUser(name);
    }
}

public static class FeatureFlagUserExtension
{
    public static FeatureFlagUser AddAttribute(this FeatureFlagUser featureFlagUser, string attribute, string value)
    {
        featureFlagUser.Attributes.Add(attribute, value);
        return featureFlagUser;
    }
}
