namespace SimpleFlag.Core.Models;
internal class FeatureFlagVariantBoolean : FeatureFlagVariant
{
    public FeatureFlagVariantBoolean(bool value) : base(FeatureFlagTypes.Boolean, value.ToString())
    {
    }
}
