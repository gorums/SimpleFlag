using SimpleFlag.Core;

namespace SimpleFlag.PostgreSQL;
public class PostgresSimpleFlagOptionsBuilder
{
    private SimpleFlagOptionsBuilder _simpleFlagOptionsBuilder;

    public string? ConnectionString
    {
        set
        {
            _simpleFlagOptionsBuilder.AddConnectionString(value);
        }
    }

    public string? SchemaName
    {
        set
        {
            _simpleFlagOptionsBuilder.AddSchemaName(value);
        }
    }

    public string? TablePrefix
    {
        set
        {
            _simpleFlagOptionsBuilder.AddTablePrefix(value);
        }
    }

    public PostgresSimpleFlagOptionsBuilder(SimpleFlagOptionsBuilder simpleFlagOptionsBuilder)
    {
        _simpleFlagOptionsBuilder = simpleFlagOptionsBuilder;
        _simpleFlagOptionsBuilder.AddDatabaseMigration(PostgresDatabaseMigration.Instance);
    }
}
