using FluentMigrator;
using SimpleFlag.PostgreSQL.Migrations.Metadata;

namespace SimpleFlag.PostgreSQL.Migrations;

[Migration(20240502121804)] // Ensure the timestamp part (20240502121805) is unique
public class AddFeatureFlagSegmentTable : Migration
{
    public override void Up()
    {
        var table = Create.Table($"{CustomMigrationMetaData.TablePrefix}_feature_flag_segments");

        table.WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Name").AsString()
            .WithColumn("Description").AsString().Nullable();

        // Assuming a separate table for Users and a many-to-many relationship
        // This creates a linking table between segments and users

        var table2 = Create.Table($"{CustomMigrationMetaData.TablePrefix}_feature_flag_segment_users");

        table2.WithColumn("SegmentId").AsGuid().PrimaryKey()
            .WithColumn("UserId").AsGuid().PrimaryKey();

        var table3 = Create.Table($"{CustomMigrationMetaData.TablePrefix}_feature_flag_segment_flags");
        table3.WithColumn("SegmentId").AsGuid().PrimaryKey()
            .WithColumn("FlagId").AsGuid().PrimaryKey();

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            table.InSchema(CustomMigrationMetaData.SchemaName);
            table2.InSchema(CustomMigrationMetaData.SchemaName);
            table3.InSchema(CustomMigrationMetaData.SchemaName);
        }

        CreateFeatureFlagSegmentForeignKey();
        CreateFeatureFlagSegmentManyToManyUsersForeignKey();
        CreateFeatureFlagSegmentManyToManyFlagsForeignKey();
    }

    private void CreateFeatureFlagSegmentForeignKey()
    {
        var table = Create
                   .ForeignKey($"fk_{CustomMigrationMetaData.TablePrefix}_feature_flags_segmentid_{CustomMigrationMetaData.TablePrefix}_feature_flag_segments_id")
                   .FromTable($"{CustomMigrationMetaData.TablePrefix}_feature_flag_segment_users");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            table.InSchema(CustomMigrationMetaData.SchemaName);
        }

        var fkTable = table.ForeignColumn("SegmentId")
            .ToTable($"{CustomMigrationMetaData.TablePrefix}_feature_flag_segments");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            fkTable.InSchema(CustomMigrationMetaData.SchemaName);
        }

        fkTable.PrimaryColumn("Id");
    }

    private void CreateFeatureFlagSegmentManyToManyUsersForeignKey()
    {
        var table = Create
                   .ForeignKey($"fk_{CustomMigrationMetaData.TablePrefix}_feature_flags_userid_{CustomMigrationMetaData.TablePrefix}_feature_flag_users_id")
                   .FromTable($"{CustomMigrationMetaData.TablePrefix}_feature_flag_segment_users");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            table.InSchema(CustomMigrationMetaData.SchemaName);
        }

        var fkTable = table.ForeignColumn("UserId")
            .ToTable($"{CustomMigrationMetaData.TablePrefix}_feature_flag_users");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            fkTable.InSchema(CustomMigrationMetaData.SchemaName);
        }

        fkTable.PrimaryColumn("Id");
    }

    private void CreateFeatureFlagSegmentManyToManyFlagsForeignKey()
    {
        var table = Create
                   .ForeignKey($"fk_{CustomMigrationMetaData.TablePrefix}_feature_flags_flagid_{CustomMigrationMetaData.TablePrefix}_feature_flag_flags_id")
                   .FromTable($"{CustomMigrationMetaData.TablePrefix}_feature_flag_segment_flags");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            table.InSchema(CustomMigrationMetaData.SchemaName);
        }

        var fkTable = table.ForeignColumn("FlagId")
            .ToTable($"{CustomMigrationMetaData.TablePrefix}_feature_flags");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            fkTable.InSchema(CustomMigrationMetaData.SchemaName);
        }

        fkTable.PrimaryColumn("Id");
    }

    public override void Down()
    {
        // Drop the linking table first due to the foreign key constraint
        var table1 = Delete.Table($"{CustomMigrationMetaData.TablePrefix}_feature_flag_segments");
        var table2 = Delete.Table($"{CustomMigrationMetaData.TablePrefix}_feature_flag_segment_users");
        var table3 = Delete.Table($"{CustomMigrationMetaData.TablePrefix}_feature_flag_segment_flags");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            table1.InSchema(CustomMigrationMetaData.SchemaName);
            table2.InSchema(CustomMigrationMetaData.SchemaName);
            table3.InSchema(CustomMigrationMetaData.SchemaName);
        }
    }
}
