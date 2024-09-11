namespace SimpleFlag.Core.Entities;

/// <summary>
/// Represents the user of the feature flag.
/// </summary>
public class FeatureFlagUser
{
    public Guid Id { get; }

    public string Name { get; }

    public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();

    public List<FeatureFlagSegment> Segments { get; set; } = new List<FeatureFlagSegment>();

    public FeatureFlagUser(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}
