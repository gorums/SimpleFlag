using SimpleFlag.Core;
using SimpleFlag.Core.DataSource;

namespace SimpleFlag;

/// <summary>
/// This class is the implementation of the ISimpleFlagService.
/// </summary>
public class SimpleFlagService : ISimpleFlagService
{
    private readonly ISimpleFlagDataSource _simpleFlagDataSource;
    private readonly SimpleFlagOptions _simpleFlagOptions;

    /// <summary>
    /// Initializes a new instance of the SimpleFlagService.
    /// </summary>
    /// <param name="simpleFlagDataSource"><see cref="ISimpleFlagDataSource"/></param>
    /// <param name="simpleFlagOptions"><see cref="SimpleFlagOptions"/></param>
    public SimpleFlagService(ISimpleFlagDataSource simpleFlagDataSource, SimpleFlagOptions simpleFlagOptions)
    {
        _simpleFlagDataSource = simpleFlagDataSource;
        _simpleFlagOptions = simpleFlagOptions;
    }

    /// <inheritdoc />
    public Task<bool> EvaluateAsync(string flag, CancellationToken cancellationToken = default)
    {
        return _simpleFlagDataSource.EvaluateAsync(flag, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<(bool success, bool result)> TryEvaluateAsync(string flag, CancellationToken cancellationToken = default)
    {
        try
        {
            return (true, await EvaluateAsync(flag, cancellationToken));
        }
        catch (SimpleFlagDoesNotExistException)
        {
            return (false, false);
        }
    }
}
