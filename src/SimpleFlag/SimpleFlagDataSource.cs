using SimpleFlag.Core;
using SimpleFlag.Core.DataSource;

namespace SimpleFlag;

public class SimpleFlagDataSource : ISimpleFlagDataSource
{
    private SimpleFlagDataSourceOptions _simpleFlagDataSourceOptions;
    private ISimpleFlagDataSourceMigration _dataSourceMigration;
    private ISimpleFlagDataSourceRepository _dataSourceRepository;

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

    public void RunMigration() => _dataSourceMigration.Run();

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