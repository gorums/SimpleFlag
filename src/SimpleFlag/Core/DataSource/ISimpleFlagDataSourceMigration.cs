namespace SimpleFlag.Core.DataSource;
public interface ISimpleFlagDataSourceMigration
{
    SimpleFlagMigrationOptions SimpleFlagMigrationOptions { get; set; }

    void Run();
}
