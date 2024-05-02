using SimpleFlag.Core.DataSource;

namespace SimpleFlag;

public class SimpleFlagDataSource : ISimpleFlagDataSource
{
    private SimpleFlagDataSourceOptions _simpleFlagDataSourceOptions;

    public SimpleFlagDataSource(SimpleFlagDataSourceOptions simpleFlagDataSourceOptions)
    {
        _simpleFlagDataSourceOptions = simpleFlagDataSourceOptions;
    }

    public void RunMigration()
    {
        _simpleFlagDataSourceOptions.DatabaseMigration?.Run(_simpleFlagDataSourceOptions);
    }
}