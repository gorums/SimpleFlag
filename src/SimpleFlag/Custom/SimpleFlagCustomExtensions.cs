using SimpleFlag.Core;

namespace SimpleFlag.Custom;

/// <summary>
/// This class contains the extension methods for the SimpleFlagOptionsBuilder.
/// </summary>
public static class SimpleFlagCustomExtensions
{
    /// <summary>
    /// Use the custom options.
    /// </summary>
    /// <param name="simpleFlagOptionsBuilder"><see cref="SimpleFlagOptionsBuilder"/></param>
    /// <param name="simpleFlagOptionsCustomDataSourceBuilder">The custom options</param>
    public static void UseCustom(this SimpleFlagOptionsBuilder simpleFlagOptionsBuilder, Action<SimpleFlagCustomOptionsBuilder>? simpleFlagOptionsCustomDataSourceBuilder) =>
        simpleFlagOptionsCustomDataSourceBuilder?.Invoke(new SimpleFlagCustomOptionsBuilder(simpleFlagOptionsBuilder));
}
