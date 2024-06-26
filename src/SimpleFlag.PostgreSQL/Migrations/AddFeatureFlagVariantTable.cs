using FluentMigrator;
using SimpleFlag.PostgreSQL.Migrations.Metadata;

namespace SimpleFlag.PostgreSQL.Migrations;

[Migration(20240502121803)] // Ensure the timestamp part (20240502121803) is unique
public class AddFeatureFlagVariantTable : Migration
{
    public override void Up()
    {
        var table = Create.Table($"{CustomMigrationMetaData.TablePrefix}_feature_flag_variants");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            table.InSchema(CustomMigrationMetaData.SchemaName);
        }

        table.WithColumn("Id").AsGuid().PrimaryKey().Identity()
            .WithColumn("Type").AsInt32() // Assuming FeatureFlagTypes is an enum
            .WithColumn("Value").AsString()
            .WithColumn("RolloutPercentage").AsInt32();
    }

    public override void Down()
    {
        var table = Delete.Table($"{CustomMigrationMetaData.TablePrefix}_feature_flag_variants");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            table.InSchema(CustomMigrationMetaData.SchemaName);
        }
    }
}
