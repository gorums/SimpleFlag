using FluentMigrator;
using SimpleFlag.PostgreSQL.Migrations.Metadata;

namespace SimpleFlag.PostgreSQL.Migrations;

[Migration(20240502121805)] // Ensure the timestamp part (20240502121805) is unique
public class AddFeatureFlagSegmentTable : Migration
{
    public override void Up()
    {
        var table = Create.Table($"{CustomMigrationMetaData.TablePrefix}_feature_flag_segments");

        table.WithColumn("Id").AsGuid().PrimaryKey().Identity()
            .WithColumn("Name").AsString();

        // Assuming a separate table for Users and a many-to-many relationship
        // This creates a linking table between segments and users

        var table2 = Create.Table($"{CustomMigrationMetaData.TablePrefix}_feature_flag_segment_users");

        table2.WithColumn("SegmentId").AsGuid().Identity()
            .WithColumn("UserId").AsGuid().Identity();

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            table.InSchema(CustomMigrationMetaData.SchemaName);
            table2.InSchema(CustomMigrationMetaData.SchemaName);
        }

        CreateFeatureFlagSegmentForeignKey();
        CreateFeatureFlagUserForeignKey();
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

    private void CreateFeatureFlagUserForeignKey()
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

    public override void Down()
    {
        // Drop the linking table first due to the foreign key constraint
        Delete.Table($"{CustomMigrationMetaData.TablePrefix}_feature_flag_segment_users");

        var table = Delete.Table($"{CustomMigrationMetaData.TablePrefix}_feature_flag_segments");

        if (!string.IsNullOrEmpty(CustomMigrationMetaData.SchemaName))
        {
            table.InSchema(CustomMigrationMetaData.SchemaName);
        }
    }
}
