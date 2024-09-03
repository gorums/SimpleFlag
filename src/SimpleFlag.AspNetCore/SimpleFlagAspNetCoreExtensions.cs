using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleFlag.AspNetCore;

/// <summary>
/// This class contains extension methods for configuring SimpleFlag endpoints in ASP.NET Core.
/// </summary>
public static class SimpleFlagAspNetCoreExtensions
{
    /// <summary>
    /// Adds the SimpleFlag endpoint data source to the service collection.
    /// Optionally, allows configuring the SimpleFlag endpoint options.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="setupAction"></param>
    /// <returns></returns>
    public static IServiceCollection AddEndpointsSimpleFlag(this IServiceCollection services, Action<SimpleFlagEndpointOptions>? setupAction = null)
    {
        services.AddSingleton<SimpleFlagEndpointDataSource>();
        if (setupAction is not null)
        {
            services.Configure(setupAction);
        }

        return services;
    }

    /// <summary>
    /// Maps the SimpleFlag endpoints to the endpoint route builder.
    /// </summary>
    /// <param name="endpoints"></param>
    /// <exception cref="Exception">Throws an exception if the SimpleFlag endpoint data source is not registered.</exception>
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
