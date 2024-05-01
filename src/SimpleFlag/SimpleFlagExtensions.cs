﻿using Microsoft.Extensions.DependencyInjection;
using SimpleFlag.Core;
using SimpleFlag.Core.DataSource;

namespace SimpleFlag;

public static class SimpleFlagExtensions
{
    public static IServiceCollection AddSimpleFlag(this IServiceCollection serviceCollection, Action<SimpleFlagOptionsBuilder>? configureOptions = null)
    {
        // setting the options
        var simpleFlagOptionsBuilder = new SimpleFlagOptionsBuilder();
        configureOptions?.Invoke(simpleFlagOptionsBuilder);

        ISimpleFlagDataSource simpleFlagDataSource = new SimpleFlagDataSource(simpleFlagOptionsBuilder.BuildDataSourceOptions());

        // adding the service
        serviceCollection.AddSingleton<ISimpleFlagService>
        (
            new SimpleFlagService(simpleFlagDataSource, simpleFlagOptionsBuilder.BuildServiceOptions())
        );

        // TODO: check if is necessary to run the migration here
        simpleFlagDataSource.RunMigration();

        return serviceCollection;
    }
}
