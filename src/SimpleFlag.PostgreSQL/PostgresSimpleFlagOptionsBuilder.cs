﻿using SimpleFlag.Core;

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

    public string PrefixSchema
    {
        set
        {
            _simpleFlagOptionsBuilder.AddPrefixSchema(value);
        }
    }

    public PostgresSimpleFlagOptionsBuilder(SimpleFlagOptionsBuilder simpleFlagOptionsBuilder)
    {
        _simpleFlagOptionsBuilder = simpleFlagOptionsBuilder;
        _simpleFlagOptionsBuilder.AddDatabaseMigration(PostgresDatabaseMigration.Instance);
    }
}