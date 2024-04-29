namespace SimpleFlag.Core;
public interface IDatabaseMigration
{
    Task InitializeIfDoesNotExitAsync();
}
