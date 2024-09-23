namespace SimpleFlag;
public class SimpleFlagUser
{
    public Guid Id { get; internal set; }

    public string Name { get; }

    public Dictionary<string, string> Attributes { get; } = new Dictionary<string, string>();

    public SimpleFlagUser(string name)
    {
        Name = name;
    }
}

public static class FeatureFlagUserExtension
{
    public static SimpleFlagUser AddAttribute(this SimpleFlagUser simpleFlagUser, string attribute, string value)
    {
        simpleFlagUser.Attributes.Add(attribute, value);
        return simpleFlagUser;
    }
}
