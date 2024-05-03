namespace SimpleFlag.Core.DataSource;
public class SimpleFlagMigrationOptions
{
    public string ConnectionString { get; init; } = string.Empty;

    public string? SchemaName { get; internal set; } = string.Empty;

    public string? TablePrefix { get; internal set; }
}
