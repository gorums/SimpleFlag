using FluentMigrator;
using SimpleFlag.PostgreSQL.Migrations.Metadata;

namespace SimpleFlag.PostgreSQL.Migrations;

[Migration(20240502121802)]
public class AddFeatureFlagUserTable : Migration
{
    public override void Up()
    {
        var table = Create.Table($"{CustomMigrationMetaData.TablePrefix}_feature_flag_users");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            table.InSchema(CustomMigrationMetaData.SchemaName);
        }

        table.WithColumn("Id").AsGuid().PrimaryKey().Identity()
            .WithColumn("Name").AsString();
    }

    public override void Down()
    {
        var table = Delete.Table($"{CustomMigrationMetaData.TablePrefix}_feature_flag_users");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            table.InSchema(CustomMigrationMetaData.SchemaName);
        }
    }
}
