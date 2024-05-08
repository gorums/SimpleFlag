using Microsoft.AspNetCore.Builder;

namespace SimpleFlag.AspNetCore;

public static class SimpleFlagAspNetCoreExtensions
{
    public static IApplicationBuilder MapSimpleFlagEndpoints(this IApplicationBuilder app)
    {
        return app;
    }
}
