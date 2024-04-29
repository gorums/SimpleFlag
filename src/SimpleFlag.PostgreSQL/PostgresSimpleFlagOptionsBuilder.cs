using SimpleFlag.Core;

namespace SimpleFlag.PostgreSQL;
public class PostgresSimpleFlagOptionsBuilder
{
    private SimpleFlagOptionsBuilder _simpleFlagOptionsBuilder;
    public string ConnectionString
    {
        set
        {
            _simpleFlagOptionsBuilder.AddConnectionString(value);
        }
    }

    public PostgresSimpleFlagOptionsBuilder(SimpleFlagOptionsBuilder simpleFlagOptionsBuilder)
    {
        _simpleFlagOptionsBuilder = simpleFlagOptionsBuilder;
        _simpleFlagOptionsBuilder.AddDatabaseMigrationImpl(PostgresDatabaseMigration.Instance);
    }
}
