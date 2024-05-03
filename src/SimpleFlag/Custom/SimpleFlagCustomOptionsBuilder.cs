using SimpleFlag.Core;
using SimpleFlag.Core.DataSource;

namespace SimpleFlag.Custom;
public class SimpleFlagCustomOptionsBuilder
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

    public ISimpleFlagDataSourceMigration DataSourceMigration
    {
        set
        {
            _simpleFlagOptionsBuilder.AddDataSourceMigration(value);
        }
    }

    public ISimpleFlagDataSourceRepository DataSourceRepository
    {
        set
        {
            _simpleFlagOptionsBuilder.AddDataSourceRepository(value);
        }
    }

    public SimpleFlagCustomOptionsBuilder(SimpleFlagOptionsBuilder simpleFlagOptionsBuilder)
    {
        _simpleFlagOptionsBuilder = simpleFlagOptionsBuilder;
    }
}
