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
}
