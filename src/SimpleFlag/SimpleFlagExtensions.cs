using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SimpleFlag.Core;

namespace SimpleFlag;

public static class SimpleFlagExtensions
{
    public static IServiceCollection AddSimpleFlag(this IServiceCollection serviceCollection, Action<SimpleFlagOptionsBuilder>? configureOptions = null)
    {
        // setting the options
        var simpleFlagOptionsBuilder = new SimpleFlagOptionsBuilder();
        configureOptions?.Invoke(simpleFlagOptionsBuilder);
        SimpleFlagOptions simpleFlagOptions = simpleFlagOptionsBuilder.Build();

        // adding the service
        serviceCollection.AddSingleton<ISimpleFlagService>(new SimpleFlagService(simpleFlagOptions));

        return serviceCollection;
    }

    public static IApplicationBuilder MapSimpleFlagEndpoints(this IApplicationBuilder app)
    {
        return app;
    }
}
