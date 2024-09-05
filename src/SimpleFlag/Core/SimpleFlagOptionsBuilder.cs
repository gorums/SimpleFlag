using SimpleFlag.Core.DataSource;
using SimpleFlag.Core.DataSource.Internal;

namespace SimpleFlag.Core;

/// <summary>
/// This class contains the general options builder for the SimpleFlag.
/// </summary>
public class SimpleFlagOptionsBuilder
{
    private string? _connectionString;
    private string? _schemaName;
    private string? _tablePrefix;

    private ISimpleFlagDataSourceMigration? _dataSourceMigration;
    private ISimpleFlagDataSourceRepository? _dataSourceRepository;

    /// <summary>
    /// Add the data source migration.
    /// </summary>
    /// <param name="dataSourceMigration"><see cref="ISimpleFlagDataSourceMigration"/></param>
    internal void AddDataSourceMigration(ISimpleFlagDataSourceMigration dataSourceMigration)
    {
        _dataSourceMigration = dataSourceMigration;
    }

    /// <summary>
    /// Add the data source repository.
    /// </summary>
    /// <param name="dataSourceRepository"><see cref="ISimpleFlagDataSourceRepository"/></param>
    internal void AddDataSourceRepository(ISimpleFlagDataSourceRepository dataSourceRepository)
    {
        _dataSourceRepository = dataSourceRepository;
    }

    /// <summary>
    /// Add the connection string.
    /// </summary>
    /// <param name="connectionString">The connection string</param>
    internal void AddConnectionString(string? connectionString)
    {
        _connectionString = connectionString;
    }

    /// <summary>
    /// Add the schema name.
    /// </summary>
    /// <param name="schemaName">The schema name</param>
    internal void AddSchemaName(string? schemaName)
    {
        _schemaName = schemaName;
    }

    /// <summary>
    /// Add the table prefix.
    /// </summary>
    /// <param name="tablePrefix">The Table prefix</param>
    internal void AddTablePrefix(string? tablePrefix)
    {
        _tablePrefix = tablePrefix;
    }

    /// <summary>
    /// Build the data source options.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentException">If the connection string is null</exception>
    /// <exception cref="ArgumentNullException">If the <see cref="ISimpleFlagDataSourceMigration"/> or the <see cref="ISimpleFlagDataSourceRepository" are null/></exception>
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
}