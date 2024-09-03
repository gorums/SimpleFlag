using SimpleFlag.Core.Models;

namespace SimpleFlag.Core.DataSource;

/// <summary>
/// This interface defines the contract for the SimpleFlagDataSourceRepository.
/// </summary>
public interface ISimpleFlagDataSourceRepository
{
    /// <summary>
    /// The options for the repository.
    /// </summary>
    SimpleFlagRepositoryOptions SimpleFlagRepositoryOptions { get; set; }

    /// <summary>
    /// Obtains the feature flag.
    /// </summary>
    /// <param name="domain">The domain</param>
    /// <param name="flag">The flag</param>
    /// <param name="user">The user</param>
    /// <param name="cancellation">The cancellation token</param>
    /// <returns>The value of the flag</returns>
    Task<FeatureFlag> GetFeatureFlagAsync(string domain, string flag, FeatureFlagUser? user, CancellationToken cancellation = default);

    /// <summary>
    /// Add a feature flag.
    /// </summary>
    /// <param name="featureFlag">The feature flag to add</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The new feature flag</returns>
    Task<FeatureFlag> AddFeatureFlagAsync(FeatureFlag featureFlag, CancellationToken cancellationToken = default);
}
