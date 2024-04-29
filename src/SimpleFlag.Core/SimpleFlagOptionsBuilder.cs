
namespace SimpleFlag.Core;

public class SimpleFlagOptionsBuilder
{
    private IDatabaseMigration _databaseMigration;
    private string _connectionString;

    internal void AddDatabaseMigrationImpl(IDatabaseMigration databaseMigration)
    {
        _databaseMigration = databaseMigration;
    }

    internal void AddConnectionString(string? connectionString)
    {
        _connectionString = connectionString;
    }

    public SimpleFlagOptions Build()
    {
        return new SimpleFlagOptions();
    }
}