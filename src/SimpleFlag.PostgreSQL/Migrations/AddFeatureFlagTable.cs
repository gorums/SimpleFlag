using FluentMigrator;
using SimpleFlag.PostgreSQL.Migrations.Metadata;

namespace SimpleFlag.PostgreSQL.Migrations;

[Migration(20240502121803)] // Ensure the timestamp part (20240502121804) is unique
public class AddFeatureFlagTable : Migration
{
    public override void Up()
    {
        var table = Create.Table($"{CustomMigrationMetaData.TablePrefix}_feature_flags");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            table.InSchema(CustomMigrationMetaData.SchemaName);
        }

        table.WithColumn("Id").AsGuid().PrimaryKey()
           .WithColumn("Name").AsString()
           .WithColumn("Description").AsString().Nullable()
           .WithColumn("Key").AsString()
           .WithColumn("Enabled").AsBoolean()
           .WithColumn("Archived").AsBoolean().WithDefaultValue(false)
           .WithColumn("DomainId").AsGuid().Nullable();

        CreateFeatureFlagDomainForeignKey();
    }

    private void CreateFeatureFlagDomainForeignKey()
    {
        var table = Create
                    .ForeignKey($"fk_{CustomMigrationMetaData.TablePrefix}_feature_flags_domainid_{CustomMigrationMetaData.TablePrefix}_feature_flag_domains_id")
                    .FromTable($"{CustomMigrationMetaData.TablePrefix}_feature_flags");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            table.InSchema(CustomMigrationMetaData.SchemaName);
        }

        var fkTable = table.ForeignColumn("DomainId")
            .ToTable($"{CustomMigrationMetaData.TablePrefix}_feature_flag_domains");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            fkTable.InSchema(CustomMigrationMetaData.SchemaName);
        }

        fkTable.PrimaryColumn("Id");
    }

    public override void Down()
    {
        var table = Delete.Table($"{CustomMigrationMetaData.TablePrefix}_feature_flags");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            table.InSchema(CustomMigrationMetaData.SchemaName);
        }
    }
}
