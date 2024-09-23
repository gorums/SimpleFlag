namespace SimpleFlag.Core.Entities;
public class FeatureFlagSegment
{
    public Guid Id { get; }

    public string Name { get; }

    public string Description { get; }

    public FeatureFlagSegment(string name, string description) : this(Guid.NewGuid(), name, description)
    {

    }

    public FeatureFlagSegment(Guid id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public List<FeatureFlagUser> Users { get; set; } = new List<FeatureFlagUser>();

    public List<FeatureFlag> Flags { get; set; } = new List<FeatureFlag>();
}
