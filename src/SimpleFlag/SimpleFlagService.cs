using SimpleFlag.Core;
using SimpleFlag.Core.DataSource;

namespace SimpleFlag;
public class SimpleFlagService : ISimpleFlagService
{
    private readonly ISimpleFlagDataSource _simpleFlagDataSource;
    private readonly SimpleFlagOptions _simpleFlagOptions;

    public SimpleFlagService(ISimpleFlagDataSource simpleFlagDataSource, SimpleFlagOptions simpleFlagOptions)
    {
        _simpleFlagDataSource = simpleFlagDataSource;
        _simpleFlagOptions = simpleFlagOptions;
    }

    public Task<bool> EvaluateAsync(string flag, CancellationToken cancellationToken = default)
    {
        return _simpleFlagDataSource.EvaluateAsync(flag, cancellationToken);
    }

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
