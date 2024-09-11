using SimpleFlag.Core.Entities;

namespace SimpleFlag.Core.Internal;

/// <summary>
/// This class is the implementation of the ISimpleFlagDataSource.
/// </summary>
internal class SimpleFlagDataSource
{
    private ISimpleFlagDataSourceMigration _dataSourceMigration;
    private ISimpleFlagDataSourceRepository _dataSourceRepository;

    /// <summary>
    /// Initializes a new instance of the SimpleFlagDataSource.
    /// </summary>
    /// <param name="simpleFlagDataSourceOptions"></param>
    /// <exception cref="ArgumentNullException"></exception>
    internal SimpleFlagDataSource(SimpleFlagDataSourceOptions simpleFlagDataSourceOptions)
    {
        _dataSourceMigration = simpleFlagDataSourceOptions.DataSourceMigration ?? throw new ArgumentNullException(nameof(simpleFlagDataSourceOptions.DataSourceMigration));
        _dataSourceRepository = simpleFlagDataSourceOptions.DataSourceRepository ?? throw new ArgumentNullException(nameof(simpleFlagDataSourceOptions.DataSourceRepository));

        _dataSourceMigration.SimpleFlagMigrationOptions = new SimpleFlagMigrationOptions
        {
            ConnectionString = simpleFlagDataSourceOptions.ConnectionString,
            SchemaName = simpleFlagDataSourceOptions.SchemaName,
            TablePrefix = simpleFlagDataSourceOptions.TablePrefix
        };

        _dataSourceRepository.SimpleFlagRepositoryOptions = new SimpleFlagRepositoryOptions
        {
            ConnectionString = simpleFlagDataSourceOptions.ConnectionString,
            SchemaName = simpleFlagDataSourceOptions.SchemaName,
            TablePrefix = simpleFlagDataSourceOptions.TablePrefix
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
    internal async Task<FeatureFlag> GetFeatureFlagAsync(string domain, string flag, SimpleFlagUser? user, CancellationToken cancellationToken)
    {
        FeatureFlagUser? featureFlagUser = user is not null ? new FeatureFlagUser(user.Name)
        {
            Attributes = new Dictionary<string, string>(user.Attributes)
        } : default;

        return await _dataSourceRepository.GetFeatureFlagAsync(domain, flag, featureFlagUser, cancellationToken) ?? throw new SimpleFlagDoesNotExistException(flag);
    }

    /// <summary>
    /// Add a feature flag.
    /// </summary>
    /// <param name="domain">The domain</param>
    /// <param name="featureFlag">The feature flag to add</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The feature flag added</returns>
    internal async Task<FeatureFlag> AddFeatureFlagAsync(string domain, FeatureFlag featureFlag, CancellationToken cancellationToken)
    {
        return await _dataSourceRepository.AddFeatureFlagAsync(domain, featureFlag, cancellationToken);
    }
}