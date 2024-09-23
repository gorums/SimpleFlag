using Microsoft.Extensions.Logging;
using SimpleFlag.Core.Entities;

namespace SimpleFlag.Core.Internal;

/// <summary>
/// This class is the implementation of the ISimpleFlagService.
/// </summary>
internal class SimpleFlagClient : ISimpleFlagClient
{
    private readonly ILogger<SimpleFlagClient>? _logger;

    private readonly SimpleFlagDataSource _simpleFlagDataSource;

    /// <summary>
    /// Initializes a new instance of the SimpleFlagService.
    /// </summary>
    /// <param name="simpleFlagDataSource"><see cref="SimpleFlagDataSource"/></param>
    /// <param name="simpleFlagOptions"><see cref="SimpleFlagOptions"/></param>
    public SimpleFlagClient(ILogger<SimpleFlagClient>? logger, SimpleFlagDataSource simpleFlagDataSource)
    {
        _logger = logger;
        _simpleFlagDataSource = simpleFlagDataSource;
    }

    /// <inheritdoc />
    public async Task<bool> GetValueAsync(string domain, string flag, bool defaultValue, SimpleFlagUser? user = null, CancellationToken cancellationToken = default)
    {
        try
        {
            return await EvaluateFeatureFlagAsync(domain, flag, user, cancellationToken);
        }
        catch
        {
            return defaultValue;
        }
    }

    /// <inheritdoc />
    public async Task<bool> GetValueAsync(string flag, bool defaultValue, SimpleFlagUser? user = null, CancellationToken cancellationToken = default)
    {
        return await GetValueAsync(string.Empty, flag, defaultValue, user, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> GetValueAsync(string domain, string flag, SimpleFlagUser? user = null, CancellationToken cancellationToken = default)
    {
        return await EvaluateFeatureFlagAsync(domain, flag, user, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> GetValueAsync(string flag, SimpleFlagUser? user = null, CancellationToken cancellationToken = default)
    {
        return await GetValueAsync(string.Empty, flag, user, cancellationToken);
    }

    /// <summary>
    /// Evaluates the feature flag.
    /// </summary>
    /// <param name="flag">The flag</param>
    /// <param name="user">The user</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>If the flag is enabled</returns>
    /// <exception cref="SimpleFlagDoesNotExistException">Thrown when the flag does not exist</exception>
    /// <exception cref="SimpleFlagUserDoesNotExistInSegmentException">Thrown when the user does not exist in the segment</exception>
    private async Task<bool> EvaluateFeatureFlagAsync(string domain, string flag, SimpleFlagUser? user, CancellationToken cancellationToken)
    {
        try
        {
            var featureFlag = await _simpleFlagDataSource.GetFeatureFlagAsync(domain, flag, user, cancellationToken);

            return featureFlag.Enabled;
        }
        catch (SimpleFlagDoesNotExistException ex)
        {
            if (_logger is not null)
            {
                _logger.LogError(ex, "The flag {Flag} does not exist", flag);
            }

            throw;
        }
        catch (SimpleFlagUserDoesNotExistInSegmentException ex)
        {
            if (_logger is not null)
            {
                _logger.LogError(ex, "The user does not exist in the feature flag segments");
            }

            throw;
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<FeatureFlag>> GetFeatureFlagsAsync(string domain, CancellationToken cancellationToken = default)
    {
        return await _simpleFlagDataSource.GetFeatureFlagsAsync(domain, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<FeatureFlag>> GetFeatureFlagsAsync(CancellationToken cancellationToken = default)
    {
        return await GetFeatureFlagsAsync(string.Empty, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<FeatureFlag> AddFeatureFlagAsync(string domain, FeatureFlag featureFlag, CancellationToken cancellationToken = default)
    {
        return await _simpleFlagDataSource.AddFeatureFlagAsync(domain, featureFlag, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<FeatureFlag> AddFeatureFlagAsync(FeatureFlag featureFlag, CancellationToken cancellationToken = default)
    {
        return await AddFeatureFlagAsync(string.Empty, featureFlag, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<FeatureFlag> UpdateFeatureFlagAsync(string domain, FeatureFlag featureFlag, CancellationToken cancellationToken = default)
    {
        return await _simpleFlagDataSource.UpdateFeatureFlagAsync(domain, featureFlag, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<FeatureFlag> UpdateFeatureFlagAsync(FeatureFlag featureFlag, CancellationToken cancellationToken = default)
    {
        return await UpdateFeatureFlagAsync(string.Empty, featureFlag, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteFeatureFlagAsync(Guid flagId, CancellationToken cancellationToken = default)
    {
        await _simpleFlagDataSource.DeleteFeatureFlagAsync(flagId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<FeatureFlagSegment>> GetSegmentsAsync(CancellationToken cancellationToken = default)
    {
        return await _simpleFlagDataSource.GetSegmentsAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<FeatureFlagSegment> AddSegmentAsync(FeatureFlagSegment segment, CancellationToken cancellationToken = default)
    {
        return await _simpleFlagDataSource.AddSegmentAsync(segment, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<FeatureFlagSegment> UpdateSegmentAsync(FeatureFlagSegment segment, CancellationToken cancellationToken = default)
    {
        return await _simpleFlagDataSource.UpdateSegmentAsync(segment, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteSegmentAsync(Guid segmentId, CancellationToken cancellationToken = default)
    {
        await _simpleFlagDataSource.DeleteSegmentAsync(segmentId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<FeatureFlagUser>> GetUsersAsync(string? segment, CancellationToken cancellationToken = default)
    {
        return await _simpleFlagDataSource.GetUsersAsync(segment, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<FeatureFlagUser>> AddUsersAsync(List<SimpleFlagUser> users, CancellationToken cancellationToken = default)
    {
        return await _simpleFlagDataSource.AddUsersAsync(users, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<SimpleFlagUser>> UpdateUsersAsync(List<SimpleFlagUser> users, CancellationToken cancellationToken = default)
    {
        return await _simpleFlagDataSource.UpdateUsersAsync(users, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteUsersAsync(List<Guid> userIds, CancellationToken cancellationToken = default)
    {
        await _simpleFlagDataSource.DeleteUsersAsync(userIds, cancellationToken);
    }

    /// <inheritdoc />
    public async Task AddSegmentToFeatureFlagAsync(string segment, Guid flagId, CancellationToken cancellationToken = default)
    {
        await _simpleFlagDataSource.AddSegmentToFeatureFlagAsync(segment, flagId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task AddUsersToSegmentAsync(List<SimpleFlagUser> users, string segment, CancellationToken cancellationToken = default)
    {
        await _simpleFlagDataSource.AddUsersToSegmentAsync(users, segment, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteUsersFromSegmentAsync(string segment, List<Guid> userIds, CancellationToken cancellationToken = default)
    {
        await _simpleFlagDataSource.DeleteUsersFromSegmentAsync(segment, userIds, cancellationToken);
    }

    /// <inheritdoc />
    public async Task CleanUsersOnSegmentAsync(string segment, CancellationToken cancellationToken = default)
    {
        await _simpleFlagDataSource.CleanUsersOnSegmentAsync(segment, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<FeatureFlagDomain>> GetDomainsAsync(CancellationToken cancellationToken = default)
    {
        return await _simpleFlagDataSource.GetDomainsAsync(cancellationToken);
    }
}
