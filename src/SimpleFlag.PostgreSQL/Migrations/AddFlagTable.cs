using FluentMigrator;

namespace SimpleFlag.PostgreSQL.Migrations;

[Migration(20240502121800)]
internal class AddFlagTable : Migration
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
        Delete.Table("sf_flags")
            .InSchema("flag");
    }
}
