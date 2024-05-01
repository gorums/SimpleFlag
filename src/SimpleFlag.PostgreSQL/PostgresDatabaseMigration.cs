using SimpleFlag.Core.DataSource;

namespace SimpleFlag.PostgreSQL;
internal class PostgresDatabaseMigration : ISimpleFlagDataSourceMigration
{
    private static readonly Lazy<ISimpleFlagDataSourceMigration> _databaseMigration = new Lazy<ISimpleFlagDataSourceMigration>(() => new PostgresDatabaseMigration());

    private PostgresDatabaseMigration()
    {
    }

    public static ISimpleFlagDataSourceMigration Instance => _databaseMigration.Value;

    public void Run(string connectionString)
    {
        // TODO: Implement database migration
    }
}