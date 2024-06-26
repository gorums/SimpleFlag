using FluentMigrator;
using SimpleFlag.PostgreSQL.Migrations.Metadata;

namespace SimpleFlag.PostgreSQL.Migrations;

[Migration(20240502121806)] // Ensure the timestamp part (20240502121806) is unique
public class AddFeatureFlagRuleTable : Migration
{
    public override void Up()
    {
        var table = Create.Table($"{CustomMigrationMetaData.TablePrefix}_feature_flag_rules");


        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            table.InSchema(CustomMigrationMetaData.SchemaName);
        }

        table.WithColumn("Id").AsGuid().PrimaryKey().Identity()
            .WithColumn("Rule").AsString()
            .WithColumn("IncludeSegmentId").AsGuid().Nullable().Identity()
            .WithColumn("ExcludeSegmentId").AsGuid().Nullable().Identity()
            .WithColumn("ValueId").AsGuid().Identity();

        CreateFeatureFlagIncludeSegmentForeignKey();
        CreateFeatureFlagExcludeSegmentForeignKey();
        CreateFeatureFlagValueForeignKey();
    }

    private void CreateFeatureFlagIncludeSegmentForeignKey()
    {
        var table = Create
                   .ForeignKey($"fk_{CustomMigrationMetaData.TablePrefix}_feature_flags_includesegmentid_{CustomMigrationMetaData.TablePrefix}_feature_flag_segments_id")
                   .FromTable($"{CustomMigrationMetaData.TablePrefix}_feature_flag_rules");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            table.InSchema(CustomMigrationMetaData.SchemaName);
        }

        var fkTable = table.ForeignColumn("IncludeSegmentId")
            .ToTable($"{CustomMigrationMetaData.TablePrefix}_feature_flag_segments");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            fkTable.InSchema(CustomMigrationMetaData.SchemaName);
        }

        fkTable.PrimaryColumn("Id");
    }

    private void CreateFeatureFlagExcludeSegmentForeignKey()
    {
        var table = Create
                   .ForeignKey($"fk_{CustomMigrationMetaData.TablePrefix}_feature_flags_excludesegmentid_{CustomMigrationMetaData.TablePrefix}_feature_flag_segments_id")
                   .FromTable($"{CustomMigrationMetaData.TablePrefix}_feature_flag_rules");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            table.InSchema(CustomMigrationMetaData.SchemaName);
        }

        var fkTable = table.ForeignColumn("ExcludeSegmentId")
            .ToTable($"{CustomMigrationMetaData.TablePrefix}_feature_flag_segments");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            fkTable.InSchema(CustomMigrationMetaData.SchemaName);
        }

        fkTable.PrimaryColumn("Id");
    }

    private void CreateFeatureFlagValueForeignKey()
    {
        var table = Create
                    .ForeignKey($"fk_{CustomMigrationMetaData.TablePrefix}_feature_flags_valueid_{CustomMigrationMetaData.TablePrefix}_feature_flag_variants_id")
                    .FromTable($"{CustomMigrationMetaData.TablePrefix}_feature_flag_rules");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            table.InSchema(CustomMigrationMetaData.SchemaName);
        }

        var fkTable = table.ForeignColumn("ValueId")
            .ToTable($"{CustomMigrationMetaData.TablePrefix}_feature_flag_variants");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            fkTable.InSchema(CustomMigrationMetaData.SchemaName);
        }

        fkTable.PrimaryColumn("Id");
    }

    public override void Down()
    {
        var table = Delete.Table($"{CustomMigrationMetaData.TablePrefix}_feature_flag_rules");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            table.InSchema(CustomMigrationMetaData.SchemaName);
        }
    }
}
