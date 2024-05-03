using SimpleFlag.Core.DataSource;

namespace DemoApi.Postgresql;

public class MyDataSourceMigration : ISimpleFlagDataSourceMigration
{
    private static readonly Lazy<ISimpleFlagDataSourceMigration> _dataSourceMigration = new Lazy<ISimpleFlagDataSourceMigration>(() => new MyDataSourceMigration());

    public SimpleFlagMigrationOptions SimpleFlagMigrationOptions { get; set; } = new SimpleFlagMigrationOptions();

    private MyDataSourceMigration()
    {
    }

    public static ISimpleFlagDataSourceMigration Instance => _dataSourceMigration.Value;

    public void Run()
    {
        //TODO: Implement database migration
    }
}
