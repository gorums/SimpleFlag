namespace SimpleFlag.Core.DataSource;
public interface ISimpleFlagDataSourceMigration
{
    void Run(string connectionString);
}
