using FluentMigrator.Runner.VersionTableInfo;

namespace SimpleFlag.PostgreSQL.Migrations;

/// <summary>
/// This class contains the custom version table meta data.
/// </summary>
[VersionTableMetaData]
internal class CustomVersionTableMetaData : IVersionTableMetaData
{
    public object? ApplicationContext { get; set; }

    public string SchemaName => CustomMigrationMetaData.SchemaName ?? string.Empty;

    public string TableName => "__SFMigrationsHistory";

    public string ColumnName => "Version";

    public string UniqueIndexName => "UC_Version";

    public string AppliedOnColumnName => "AppliedOn";

    public string DescriptionColumnName => "Description";

    public bool OwnsSchema => true;
}
