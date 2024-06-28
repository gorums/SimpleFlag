using SimpleFlag.Core;

namespace SimpleFlag.PostgreSQL;

/// <summary>
/// This class contains the options for the PostgresSimpleFlagOptionsBuilder.
/// </summary>
public class PostgresSimpleFlagOptionsBuilder
{
    private SimpleFlagOptionsBuilder _simpleFlagOptionsBuilder;

    /// <summary>
    /// Initializes a new instance of the PostgresSimpleFlagOptionsBuilder.
    /// </summary>
    /// <param name="simpleFlagOptionsBuilder"><see cref="SimpleFlagOptionsBuilder"/></param>
    public PostgresSimpleFlagOptionsBuilder(SimpleFlagOptionsBuilder simpleFlagOptionsBuilder)
    {
        _simpleFlagOptionsBuilder = simpleFlagOptionsBuilder;
        _simpleFlagOptionsBuilder.AddDataSourceMigration(PostgresDataSourceMigration.Instance);
        _simpleFlagOptionsBuilder.AddDataSourceRepository(PostgresDataSourceRepository.Instance);
    }

    /// <summary>
    /// Set the connection string.
    /// </summary>
    public string? ConnectionString
    {
        set
        {
            _simpleFlagOptionsBuilder.AddConnectionString(value);
        }
    }

    /// <summary>
    /// Set the schema name.
    /// </summary>
    public string? SchemaName
    {
        set
        {
            _simpleFlagOptionsBuilder.AddSchemaName(value);
        }
    }

    /// <summary>
    /// Set the table prefix.
    /// </summary>
    public string? TablePrefix
    {
        set
        {
            _simpleFlagOptionsBuilder.AddTablePrefix(value);
        }
    }

    /// <summary>
    /// Sets the global domain.
    /// </summary>
    public string? Domain
    {
        set
        {
            _simpleFlagOptionsBuilder.AddDomain(value);
        }
    }
}
