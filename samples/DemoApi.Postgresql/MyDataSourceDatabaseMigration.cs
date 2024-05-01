using SimpleFlag.Core.DataSource;

namespace DemoApi.Postgresql;

public class MyDataSourceDatabaseMigration : ISimpleFlagDataSourceMigration
{
    private static readonly Lazy<ISimpleFlagDataSourceMigration> _databaseMigration = new Lazy<ISimpleFlagDataSourceMigration>(() => new MyDataSourceDatabaseMigration());

    private MyDataSourceDatabaseMigration()
    {
    }

    public static ISimpleFlagDataSourceMigration Instance => _databaseMigration.Value;

    public void Run(string connectionString)
    {
        //TODO: Implement database migration
    }
}
