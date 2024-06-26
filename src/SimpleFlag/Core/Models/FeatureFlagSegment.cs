namespace SimpleFlag.Core.Models;
public class FeatureFlagSegment
{
    public Guid Id { get; }

    public string Name { get; }

    public FeatureFlagSegment(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public List<FeatureFlagUser> Users { get; set; } = new List<FeatureFlagUser>();
}
