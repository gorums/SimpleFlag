using Microsoft.AspNetCore.Builder;

namespace SimpleFlag.AspNetCore;

/// <summary>
/// This class contains the extension methods for the IApplicationBuilder.
/// </summary>
public static class SimpleFlagAspNetCoreExtensions
{
    /// <summary>
    /// Maps the SimpleFlag endpoints.
    /// </summary>
    /// <param name="app"><see cref="IApplicationBuilder"/></param>
    /// <returns><see cref="IApplicationBuilder"/></returns>
    public static IApplicationBuilder MapSimpleFlagEndpoints(this IApplicationBuilder app)
    {
        return app;
    }
}
