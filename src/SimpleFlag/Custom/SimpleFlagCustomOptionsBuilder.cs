﻿using SimpleFlag.Core;

namespace SimpleFlag.Custom;

/// <summary>
/// This class builds the custom options for the SimpleFlag.
/// </summary>
public class SimpleFlagCustomOptionsBuilder
{
    private SimpleFlagOptionsBuilder _simpleFlagOptionsBuilder;

    /// <summary>
    /// Initializes a new instance of the SimpleFlagCustomOptionsBuilder.
    /// </summary>
    /// <param name="simpleFlagOptionsBuilder"><see cref="SimpleFlagOptionsBuilder"/></param>
    public SimpleFlagCustomOptionsBuilder(SimpleFlagOptionsBuilder simpleFlagOptionsBuilder)
    {
        _simpleFlagOptionsBuilder = simpleFlagOptionsBuilder;
    }

    public string? ConnectionString
    {
        set
        {
            _simpleFlagOptionsBuilder.AddConnectionString(value);
        }
    }

    /// <summary>
    /// Sets the schema name.
    /// </summary>
    public string? SchemaName
    {
        set
        {
            _simpleFlagOptionsBuilder.AddSchemaName(value);
        }
    }

    /// <summary> 
    /// Sets the table prefix.
    /// </summary>
    public string? TablePrefix
    {
        set
        {
            _simpleFlagOptionsBuilder.AddTablePrefix(value);
        }
    }

    /// <summary>
    /// Sets the data source migration.
    /// </summary>
    public ISimpleFlagDataSourceMigration Migrator
    {
        set
        {
            _simpleFlagOptionsBuilder.AddDataSourceMigration(value);
        }
    }

    /// <summary>
    /// Sets the data source repository.
    /// </summary>
    public ISimpleFlagDataSourceRepository Repository
    {
        set
        {
            _simpleFlagOptionsBuilder.AddDataSourceRepository(value);
        }
    }
}
