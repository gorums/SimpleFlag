using SimpleFlag.Core;

namespace SimpleFlag;

public class SimpleFlagService : ISimpleFlagService
{
    private SimpleFlagOptions _simpleFlagOptions;

    public SimpleFlagService(SimpleFlagOptions simpleFlagOptions)
    {
        _simpleFlagOptions = simpleFlagOptions;
    }
}