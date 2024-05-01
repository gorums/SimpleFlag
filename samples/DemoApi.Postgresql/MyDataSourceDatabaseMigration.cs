using SimpleFlag.Core;

namespace DemoApi.Postgresql;

public class MyDataSourceDatabaseMigration : IDatabaseMigration
{
    private static readonly Lazy<IDatabaseMigration> _databaseMigration = new Lazy<IDatabaseMigration>(() => new MyDataSourceDatabaseMigration());

    private MyDataSourceDatabaseMigration()
    {
    }

    public static IDatabaseMigration Instance => _databaseMigration.Value;

    public void Run(string connectionString)
    {
        //TODO: Implement database migration
    }
}
