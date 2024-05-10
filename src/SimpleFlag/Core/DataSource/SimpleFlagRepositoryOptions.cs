namespace SimpleFlag.Core.DataSource;

/// <summary>
/// This class contains the options for the SimpleFlagMigration.
/// </summary>
public class SimpleFlagMigrationOptions
{
    public string ConnectionString { get; init; } = string.Empty;

    public string? SchemaName { get; internal set; } = string.Empty;

    public string? TablePrefix { get; internal set; }
}
