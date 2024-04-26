using Microsoft.Extensions.DependencyInjection;
using SimpleFlag.Core;

namespace SimpleFlag;

public static class SimpleFlagExtensions
{
    public static IServiceCollection AddSimpleFlag(this IServiceCollection serviceCollection, Action<SimpleFlagOptionsBuilder> configureOptions)
    {
        serviceCollection.AddSingleton<ISimpleFlagService, SimpleFlagService>();

        return serviceCollection;
    }
}
