using Microsoft.Extensions.Logging;
using SimpleFlag.Core;
using SimpleFlag.Core.DataSource.Internal;
using SimpleFlag.Core.Models;

namespace SimpleFlag;

/// <summary>
/// This class is the implementation of the ISimpleFlagService.
/// </summary>
internal class SimpleFlagClient : ISimpleFlagClient
{
    private readonly ILogger<SimpleFlagClient>? _logger;

    private readonly SimpleFlagDataSource _simpleFlagDataSource;
    private readonly SimpleFlagOptions _simpleFlagOptions;

    private string _domain;

    /// <summary>
    /// Initializes a new instance of the SimpleFlagService.
    /// </summary>
    /// <param name="simpleFlagDataSource"><see cref="SimpleFlagDataSource"/></param>
    /// <param name="simpleFlagOptions"><see cref="SimpleFlagOptions"/></param>
    public SimpleFlagClient(ILogger<SimpleFlagClient>? logger, SimpleFlagDataSource simpleFlagDataSource, SimpleFlagOptions simpleFlagOptions)
    {
        _logger = logger;
        _simpleFlagDataSource = simpleFlagDataSource;
        _simpleFlagOptions = simpleFlagOptions;

        _domain = _simpleFlagOptions.Domain;
    }

    /// <inheritdoc />
    public ISimpleFlagClient Domain(string domain)
    {
        _domain = domain;
        return this;
    }

    /// <inheritdoc />
    public async Task<bool> GetValueAsync(string flag, bool defaultValue, FeatureFlagUser? user = null, CancellationToken cancellationToken = default)
    {
        try
        {
            return await EvaluateFeatureFlagAsync(flag, user, cancellationToken);
        }
        catch
        {
            return defaultValue;
        }
    }

    /// <inheritdoc />
    public async Task<bool> GetValueAsync(string flag, FeatureFlagUser? user = null, CancellationToken cancellationToken = default)
    {
        return await EvaluateFeatureFlagAsync(flag, user, cancellationToken);
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
    private async Task<bool> EvaluateFeatureFlagAsync(string flag, FeatureFlagUser? user, CancellationToken cancellationToken)
    {
        try
        {
            var featureFlag = await _simpleFlagDataSource.GetFeatureFlagAsync(_domain, flag, user, cancellationToken);

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
}
