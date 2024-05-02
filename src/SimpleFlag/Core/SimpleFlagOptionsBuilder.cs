using SimpleFlag.Core.DataSource;

namespace SimpleFlag.Core;

public class SimpleFlagOptionsBuilder
{
    private ISimpleFlagDataSourceMigration? _databaseMigration;
    private string? _connectionString;
    private string? _schemaName;
    private string? _tablePrefix;

    internal void AddDatabaseMigration(ISimpleFlagDataSourceMigration databaseMigration)
    {
        _databaseMigration = databaseMigration;
    }

    internal void AddConnectionString(string? connectionString)
    {
        _connectionString = connectionString;
    }

    internal void AddSchemaName(string? schemaName)
    {
        _schemaName = schemaName;
    }

    internal void AddTablePrefix(string? tablePrefix)
    {
        _tablePrefix = tablePrefix;
    }

    internal SimpleFlagDataSourceOptions BuildDataSourceOptions()
    {
        return new SimpleFlagDataSourceOptions
        {
            ConnectionString = _connectionString ?? throw new ArgumentException("The connection string is required."),
            SchemaName = _schemaName,
            TablePrefix = _tablePrefix,
            DatabaseMigration = _databaseMigration ?? throw new NotImplementedException($"{nameof(ISimpleFlagDataSourceMigration)} does not have implementation.")
        };
    }

    internal SimpleFlagOptions BuildServiceOptions()
    {
        return new SimpleFlagOptions
        {
        };
    }
}