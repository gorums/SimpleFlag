namespace SimpleFlag.Core.Internal;

/// <summary>
/// This class contains the options for the SimpleFlagDataSource.
/// </summary>
internal class SimpleFlagDataSourceOptions
{
    internal string ConnectionString { get; init; } = string.Empty;

    internal ISimpleFlagDataSourceMigration? DataSourceMigration { get; init; }

    internal ISimpleFlagDataSourceRepository? DataSourceRepository { get; init; }

    internal string? SchemaName { get; set; } = string.Empty;

    internal string? TablePrefix { get; set; }
}
