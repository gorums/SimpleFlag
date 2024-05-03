using SimpleFlag.Core.DataSource;

namespace SimpleFlag.Core;

public class SimpleFlagOptionsBuilder
{
    private string? _connectionString;
    private string? _schemaName;
    private string? _tablePrefix;

    private ISimpleFlagDataSourceMigration? _dataSourceMigration;
    private ISimpleFlagDataSourceRepository? _dataSourceRepository;    

    internal void AddDataSourceMigration(ISimpleFlagDataSourceMigration dataSourceMigration)
    {
        _dataSourceMigration = dataSourceMigration;
    }

    internal void AddDataSourceRepository(ISimpleFlagDataSourceRepository dataSourceRepository)
    {
        _dataSourceRepository = dataSourceRepository;
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
            SchemaName = _schemaName,
            TablePrefix = _tablePrefix,
            ConnectionString = _connectionString ?? throw new ArgumentException(nameof(_connectionString)),            
            DataSourceMigration = _dataSourceMigration ?? throw new ArgumentNullException(nameof(_dataSourceMigration)),
            DataSourceRepository = _dataSourceRepository ?? throw new ArgumentNullException(nameof(_dataSourceRepository)),
        };
    }

    internal SimpleFlagOptions BuildServiceOptions()
    {
        return new SimpleFlagOptions
        {
        };
    }
}