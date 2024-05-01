using SimpleFlag.Core;

namespace SimpleFlag.CustomDataSource;
public class SimpleFlagCustomDataSourceOptionsBuilder
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

    public IDatabaseMigration DatabaseMigration
    {
        set
        {
            _simpleFlagOptionsBuilder.AddDatabaseMigration(value);
        }
    }

    public SimpleFlagCustomDataSourceOptionsBuilder(SimpleFlagOptionsBuilder simpleFlagOptionsBuilder)
    {
        _simpleFlagOptionsBuilder = simpleFlagOptionsBuilder;
    }
}
