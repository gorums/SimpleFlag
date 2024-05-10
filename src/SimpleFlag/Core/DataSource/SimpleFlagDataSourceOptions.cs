namespace SimpleFlag.Core.DataSource;

/// <summary>
/// This class contains the options for the SimpleFlagDataSource.
/// </summary>
internal class SimpleFlagDataSourceOptions
{
    public string ConnectionString { get; init; } = string.Empty;

    public ISimpleFlagDataSourceMigration? DataSourceMigration { get; init; }

    public ISimpleFlagDataSourceRepository? DataSourceRepository { get; init; }

    public string? SchemaName { get; internal set; } = string.Empty;

    public string? TablePrefix { get; internal set; }
}
