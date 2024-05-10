using SimpleFlag.Core;
using SimpleFlag.Core.DataSource;

namespace SimpleFlag;

/// <summary>
/// This class is the implementation of the ISimpleFlagDataSource.
/// </summary>
public class SimpleFlagDataSource : ISimpleFlagDataSource
{
    private SimpleFlagDataSourceOptions _simpleFlagDataSourceOptions;
    private ISimpleFlagDataSourceMigration _dataSourceMigration;
    private ISimpleFlagDataSourceRepository _dataSourceRepository;

    /// <summary>
    /// Initializes a new instance of the SimpleFlagDataSource.
    /// </summary>
    /// <param name="simpleFlagDataSourceOptions"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public SimpleFlagDataSource(SimpleFlagDataSourceOptions simpleFlagDataSourceOptions)
    {
        _simpleFlagDataSourceOptions = simpleFlagDataSourceOptions;
        _dataSourceMigration = _simpleFlagDataSourceOptions.DataSourceMigration ?? throw new ArgumentNullException(nameof(simpleFlagDataSourceOptions.DataSourceMigration));
        _dataSourceRepository = _simpleFlagDataSourceOptions.DataSourceRepository ?? throw new ArgumentNullException(nameof(simpleFlagDataSourceOptions.DataSourceRepository));

        _dataSourceMigration.SimpleFlagMigrationOptions = new SimpleFlagMigrationOptions
        {
            ConnectionString = _simpleFlagDataSourceOptions.ConnectionString,
            SchemaName = _simpleFlagDataSourceOptions.SchemaName,
            TablePrefix = _simpleFlagDataSourceOptions.TablePrefix
        };

        _dataSourceRepository.SimpleFlagRepositoryOptions = new SimpleFlagRepositoryOptions
        {
            ConnectionString = _simpleFlagDataSourceOptions.ConnectionString,
            SchemaName = _simpleFlagDataSourceOptions.SchemaName,
            TablePrefix = _simpleFlagDataSourceOptions.TablePrefix
        };
    }

    /// <inheritdoc />
    public void RunMigration() => _dataSourceMigration.Run();

    /// <inheritdoc />
    public async Task<bool> EvaluateAsync(string flag, CancellationToken cancellationToken)
    {
        var value = await _dataSourceRepository.GetFlagValueAsync(flag, cancellationToken)!;

        if (value is null)
        {
            throw new SimpleFlagDoesNotExistException(flag);
        }

        return bool.TryParse(value, out bool result) && result;
    }
}