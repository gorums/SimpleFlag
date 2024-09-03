using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using SimpleFlag.Core.DataSource;
using SimpleFlag.PostgreSQL.Migrations;
using SimpleFlag.PostgreSQL.Migrations.Metadata;

namespace SimpleFlag.PostgreSQL;

/// <summary>
/// This class is responsible for migrating the database.
/// </summary>
internal class PostgresDataSourceMigration : ISimpleFlagDataSourceMigration
{
    private static readonly Lazy<ISimpleFlagDataSourceMigration> _databaseMigration = new Lazy<ISimpleFlagDataSourceMigration>(() => new PostgresDataSourceMigration());

    /// <summary>
    /// The options for the migration.
    /// </summary>
    public SimpleFlagMigrationOptions SimpleFlagMigrationOptions { get; set; } = new SimpleFlagMigrationOptions();

    /// <summary>
    /// Private constructor to prevent instantiation.
    /// </summary>
    private PostgresDataSourceMigration()
    {
    }

    /// <inheritdoc />
    public static ISimpleFlagDataSourceMigration Instance => _databaseMigration.Value;

    /// <inheritdoc />
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
                .ScanIn(typeof(AddFeatureFlagUserTable).Assembly).For.Migrations())
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