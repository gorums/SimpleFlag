namespace SimpleFlag.Core.Entities;
public class FeatureFlagSegment
{
    public Guid Id { get; }

    public string Name { get; }

    public string Description { get; }

    public FeatureFlagSegment(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public List<FeatureFlagUser> Users { get; set; } = new List<FeatureFlagUser>();

    public List<FeatureFlag> Flags { get; set; } = new List<FeatureFlag>();
}
