
namespace SimpleFlag.Core;

public class SimpleFlagOptionsBuilder
{
    private IDatabaseMigration? _databaseMigration;
    private string? _connectionString;
    private string? _prefixSchema;

    internal void AddDatabaseMigration(IDatabaseMigration databaseMigration)
    {
        _databaseMigration = databaseMigration;
    }

    internal void AddConnectionString(string? connectionString)
    {
        _connectionString = connectionString;
    }

    internal void AddPrefixSchema(string? prefixSchema)
    {
        _prefixSchema = prefixSchema;
    }

    internal SimpleFlagDataSourceOptions BuildDataSourceOptions()
    {
        return new SimpleFlagDataSourceOptions
        {
            ConnectionString = _connectionString ?? throw new ArgumentException("The connection string is required."),
            PrefixSchema = _prefixSchema ?? "dbo.",
            DatabaseMigration = _databaseMigration ?? throw new NotImplementedException($"{nameof(IDatabaseMigration)} does not have implementation.")
        };
    }

    internal SimpleFlagOptions BuildServiceOptions()
    {
        return new SimpleFlagOptions
        {
        };
    }
}