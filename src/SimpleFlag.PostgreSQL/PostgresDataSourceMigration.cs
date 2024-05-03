using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using SimpleFlag.Core.DataSource;
using SimpleFlag.PostgreSQL.Migrations;

namespace SimpleFlag.PostgreSQL;
internal class PostgresDataSourceMigration : ISimpleFlagDataSourceMigration
{
    private static readonly Lazy<ISimpleFlagDataSourceMigration> _databaseMigration = new Lazy<ISimpleFlagDataSourceMigration>(() => new PostgresDataSourceMigration());

    private PostgresDataSourceMigration()
    {
    }

    public static ISimpleFlagDataSourceMigration Instance => _databaseMigration.Value;

    public SimpleFlagMigrationOptions SimpleFlagMigrationOptions { get; set; }

    public void Run()
    {
        using (var serviceProvider = CreateServices(SimpleFlagMigrationOptions))
        using (var scope = serviceProvider.CreateScope())
        {
            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            UpdateDatabase(scope.ServiceProvider);
        }
    }

    /// <summary>
    /// Configure the dependency injection services
    /// </summary>
    private static ServiceProvider CreateServices(SimpleFlagMigrationOptions simpleFlagDataSourceOptions)
    {
        CustomMigrationMetaData.SchemaName = simpleFlagDataSourceOptions.SchemaName;
        if (!string.IsNullOrEmpty(simpleFlagDataSourceOptions.TablePrefix))
        {
            CustomMigrationMetaData.TablePrefix = simpleFlagDataSourceOptions.TablePrefix;
        }

        return new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                // Add Postgres support to FluentMigrator
                .AddPostgres()
                // Set the connection string
                .WithGlobalConnectionString(simpleFlagDataSourceOptions.ConnectionString)
                .WithVersionTable(new CustomVersionTableMetaData())
                // Define the assembly containing the migrations
                .ScanIn(typeof(AddFlagTable).Assembly).For.Migrations())
            // Enable logging to console in the FluentMigrator way
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            // Build the service provider
            .BuildServiceProvider(false);
    }

    /// <summary>
    /// Update the database
    /// </summary>
    private static void UpdateDatabase(IServiceProvider serviceProvider)
    {
        // Instantiate the runner
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        // Execute the migrations
        runner.MigrateUp();
    }
}