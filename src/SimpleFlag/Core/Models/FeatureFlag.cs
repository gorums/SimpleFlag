namespace SimpleFlag.Core.Models;
public class FeatureFlag
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public bool Enabled { get; set; } = false;
    public bool Archived { get; set; } = false;
    public FeatureFlagDomain? Domain { get; set; }
    public List<FeatureFlagSegment> Segments { get; set; } = new List<FeatureFlagSegment>();

    public bool EvaluateUser<T>(T user) where T : FeatureFlagUser
    {
        foreach (var segment in Segments)
        {
            if (segment.Users.Any(u => u.Name.Equals(user.Name)))
            {
                return true;
            }
        }

        return false;
    }
}
