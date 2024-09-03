using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleFlag.AspNetCore;

public static class SimpleFlagAspNetCoreExtensions
{
    public static IServiceCollection AddEndpointsSimpleFlag(this IServiceCollection services, Action<SimpleFlagEndpointOptions>? setupAction = null)
    {
        services.AddSingleton<SimpleFlagEndpointDataSource>();
        if (setupAction is not null)
        {
            services.Configure(setupAction);
        }

        return services;
    }

    public static void MapSimpleFlagEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var dataSource = endpoints.ServiceProvider.GetService<SimpleFlagEndpointDataSource>();

        if (dataSource is null)
        {
            throw new Exception("Did you forget to call Services.AddMyEndpoints()?");
        }

        endpoints.DataSources.Add(dataSource);
    }
}