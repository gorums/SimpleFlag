﻿namespace SimpleFlag.Core.DataSource;
public class SimpleFlagDataSourceOptions
{
    public string ConnectionString { get; init; } = string.Empty;

    public ISimpleFlagDataSourceMigration? DataSourceMigration { get; init; }

    public ISimpleFlagDataSourceRepository? DataSourceRepository { get; init; }

    public string? SchemaName { get; internal set; } = string.Empty;

    public string? TablePrefix { get; internal set; }
}