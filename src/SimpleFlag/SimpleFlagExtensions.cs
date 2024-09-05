using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleFlag.Core;
using SimpleFlag.Core.DataSource.Internal;

namespace SimpleFlag;

/// <summary>
/// This class contains the extension methods for the IServiceCollection.
/// </summary>
public static class SimpleFlagExtensions
{
    /// <summary>
    /// Adds the SimpleFlag service to the service collection.
    /// </summary>
    /// <param name="serviceCollection"><see cref="IServiceCollection"/></param>
    /// <param name="configureOptions">The configuration options action</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddSimpleFlag(this IServiceCollection serviceCollection, Action<SimpleFlagOptionsBuilder>? configureOptions = null)
    {
        // setting the options
        var simpleFlagOptionsBuilder = new SimpleFlagOptionsBuilder();
        configureOptions?.Invoke(simpleFlagOptionsBuilder);

        var simpleFlagDataSource = new SimpleFlagDataSource(simpleFlagOptionsBuilder.BuildDataSourceOptions());

        // adding the service
        serviceCollection.AddSingleton<ISimpleFlagClient>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<SimpleFlagClient>>();
            return new SimpleFlagClient(logger, simpleFlagDataSource);
        });

        // TODO: check if is necessary to run the migration here
        simpleFlagDataSource.RunMigration();

        return serviceCollection;
    }
}
