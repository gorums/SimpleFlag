using SimpleFlag.Core;

namespace SimpleFlag.CustomDataSource;

public static class SimpleFlagCustomDataSourceExtensions
{
    public static void UseCustomDataSource(this SimpleFlagOptionsBuilder simpleFlagOptionsBuilder, Action<SimpleFlagCustomDataSourceOptionsBuilder>? simpleFlagOptionsCustomDataSourceBuilder) =>
        simpleFlagOptionsCustomDataSourceBuilder?.Invoke(new SimpleFlagCustomDataSourceOptionsBuilder(simpleFlagOptionsBuilder));
}
