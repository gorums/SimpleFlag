namespace SimpleFlag.Core;
public class SimpleFlagDataSourceOptions
{
    public string ConnectionString { get; init; }

    public IDatabaseMigration DatabaseMigration { get; init; }
    public string PrefixSchema { get; internal set; }
}
