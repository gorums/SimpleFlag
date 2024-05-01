namespace SimpleFlag.Core;
public interface IDatabaseMigration
{
    void Run(string connectionString);
}
