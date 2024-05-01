using Microsoft.AspNetCore.Builder;

namespace SimpleFlaq.AspNetCore;

public static class SimpleFlagAspNetCoreExtensions
{
    public static IApplicationBuilder MapSimpleFlagEndpoints(this IApplicationBuilder app)
    {
        return app;
    }
}
