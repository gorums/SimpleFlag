﻿using FluentMigrator;

namespace SimpleFlag.PostgreSQL.Migrations;

[Migration(20240502121801)]
public class AddFlagTable : Migration
{
    public override void Up()
    {
        var table = Create.Table($"{CustomMigrationMetaData.TablePrefix}_flags");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            table.InSchema(CustomMigrationMetaData.SchemaName);
        }

        table.WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Key").AsString()
            .WithColumn("Value").AsString();
    }

    public override void Down()
    {
        var table = Delete.Table($"{CustomMigrationMetaData.TablePrefix}_flags");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            table.InSchema(CustomMigrationMetaData.SchemaName);
        }
    }
}