namespace SimpleFlag.Core.Models;
public class FeatureFlagSegment
{
    public string Id { get; }

    public string Name { get; }

    public FeatureFlagSegment(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public List<FeatureFlagUser> Users { get; set; } = new List<FeatureFlagUser>();
}
