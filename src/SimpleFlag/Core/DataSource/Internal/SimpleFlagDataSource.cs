using SimpleFlag.Core.Models;

namespace SimpleFlag.Core.DataSource.Internal;

/// <summary>
/// This class is the implementation of the ISimpleFlagDataSource.
/// </summary>
internal class SimpleFlagDataSource
{
    private SimpleFlagDataSourceOptions _simpleFlagDataSourceOptions;
    private ISimpleFlagDataSourceMigration _dataSourceMigration;
    private ISimpleFlagDataSourceRepository _dataSourceRepository;

    /// <summary>
    /// Initializes a new instance of the SimpleFlagDataSource.
    /// </summary>
    /// <param name="simpleFlagDataSourceOptions"></param>
    /// <exception cref="ArgumentNullException"></exception>
    internal SimpleFlagDataSource(SimpleFlagDataSourceOptions simpleFlagDataSourceOptions)
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

    /// <summary>
    /// Run the migration.
    /// </summary>
    internal void RunMigration() => _dataSourceMigration.Run();

    /// <summary>
    /// Obtain the feature flag if exist.
    /// </summary>
    /// <param name="domain">The domain</param>
    /// <param name="flag">The flag</param>
    /// <param name="user">The user</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>If the flag is enabled</returns>
    /// <exception cref="SimpleFlagDoesNotExistException">Thrown when the flag does not exist</exception></exception>
    internal async Task<FeatureFlag> GetFeatureFlagAsync(string domain, string flag, FeatureFlagUser? user, CancellationToken cancellationToken)
    {
        return await _dataSourceRepository.GetFeatureFlagAsync(domain, flag, user, cancellationToken) ?? throw new SimpleFlagDoesNotExistException(flag);
    }

    /// <summary>
    /// Add a feature flag.
    /// </summary>
    /// <param name="featureFlag">The feature flag to add</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The feature flag added</returns>
    internal async Task<FeatureFlag> AddFeatureFlagAsync(FeatureFlag featureFlag, CancellationToken cancellationToken)
    {
        return await _dataSourceRepository.AddFeatureFlagAsync(featureFlag, cancellationToken);
    }
}