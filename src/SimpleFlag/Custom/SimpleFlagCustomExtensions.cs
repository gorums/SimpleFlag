using SimpleFlag.Core;

namespace SimpleFlag.Custom;

public static class SimpleFlagCustomExtensions
{
    public static void UseCustom(this SimpleFlagOptionsBuilder simpleFlagOptionsBuilder, Action<SimpleFlagCustomOptionsBuilder>? simpleFlagOptionsCustomDataSourceBuilder) =>
        simpleFlagOptionsCustomDataSourceBuilder?.Invoke(new SimpleFlagCustomOptionsBuilder(simpleFlagOptionsBuilder));
}
