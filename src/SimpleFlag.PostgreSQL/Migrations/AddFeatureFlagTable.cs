using FluentMigrator;
using SimpleFlag.PostgreSQL.Migrations.Metadata;

namespace SimpleFlag.PostgreSQL.Migrations;

[Migration(20240502121804)] // Ensure the timestamp part (20240502121804) is unique
public class AddFeatureFlagTable : Migration
{
    public override void Up()
    {
        var table = Create.Table($"{CustomMigrationMetaData.TablePrefix}_feature_flags");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            table.InSchema(CustomMigrationMetaData.SchemaName);
        }

        table.WithColumn("Id").AsGuid().PrimaryKey().Identity()
           .WithColumn("Name").AsString()
           .WithColumn("Description").AsString().Nullable()
           .WithColumn("Key").AsString()
           .WithColumn("Enabled").AsBoolean()
           .WithColumn("Archived").AsBoolean().WithDefaultValue(false)
           .WithColumn("Type").AsInt32()
           .WithColumn("DomainId").AsGuid().Nullable().Identity()
           .WithColumn("DefaultOnId").AsGuid().Nullable().Identity()
           .WithColumn("DefaultOffId").AsGuid().Nullable().Identity();

        CreateFeatureFlagDomainForeignKey();
        CreateFeatureFlagVariantsDefaultOffForeignKey();
        CreateFeatureFlagVariantsDefaultOnForeignKey();
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

    private void CreateFeatureFlagVariantsDefaultOffForeignKey()
    {
        var table = Create
                    .ForeignKey($"fk_{CustomMigrationMetaData.TablePrefix}_feature_flags_defaultonid_{CustomMigrationMetaData.TablePrefix}_feature_flag_variants_id")
                    .FromTable($"{CustomMigrationMetaData.TablePrefix}_feature_flags");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            table.InSchema(CustomMigrationMetaData.SchemaName);
        }

        var fkTable = table.ForeignColumn("DefaultOnId")
            .ToTable($"{CustomMigrationMetaData.TablePrefix}_feature_flag_variants");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            fkTable.InSchema(CustomMigrationMetaData.SchemaName);
        }

        fkTable.PrimaryColumn("Id");
    }

    private void CreateFeatureFlagVariantsDefaultOnForeignKey()
    {
        var table = Create
                    .ForeignKey($"fk_{CustomMigrationMetaData.TablePrefix}_feature_flags_defaultoffid_{CustomMigrationMetaData.TablePrefix}_feature_flag_variants_id")
                    .FromTable($"{CustomMigrationMetaData.TablePrefix}_feature_flags");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            table.InSchema(CustomMigrationMetaData.SchemaName);
        }

        var fkTable = table.ForeignColumn("DefaultOffId")
            .ToTable($"{CustomMigrationMetaData.TablePrefix}_feature_flag_variants");

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
