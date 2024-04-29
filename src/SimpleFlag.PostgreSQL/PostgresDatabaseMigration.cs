using SimpleFlag.Core;

namespace SimpleFlag.PostgreSQL;
internal class PostgresDatabaseMigration : IDatabaseMigration
{
    private static readonly Lazy<IDatabaseMigration> _databaseMigration = new Lazy<IDatabaseMigration>(() => new PostgresDatabaseMigration());

    private PostgresDatabaseMigration()
    {
    }

    public static IDatabaseMigration Instance => _databaseMigration.Value;

    public Task InitializeIfDoesNotExitAsync()
    {
        throw new NotImplementedException();
    }
}