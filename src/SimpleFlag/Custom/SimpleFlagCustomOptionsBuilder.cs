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

    public string PrefixSchema
    {
        set
        {
            _simpleFlagOptionsBuilder.AddPrefixSchema(value);
        }
    }

    public ISimpleFlagDataSourceMigration DatabaseMigration
    {
        set
        {
            _simpleFlagOptionsBuilder.AddDatabaseMigration(value);
        }
    }

    public SimpleFlagCustomOptionsBuilder(SimpleFlagOptionsBuilder simpleFlagOptionsBuilder)
    {
        _simpleFlagOptionsBuilder = simpleFlagOptionsBuilder;
    }
}
