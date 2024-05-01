namespace SimpleFlag.Core.DataSource;
public class SimpleFlagDataSourceOptions
{
    public string ConnectionString { get; init; }

    public ISimpleFlagDataSourceMigration DatabaseMigration { get; init; }
    public string PrefixSchema { get; internal set; }
}
