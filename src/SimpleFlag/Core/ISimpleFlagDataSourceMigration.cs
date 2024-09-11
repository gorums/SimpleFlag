namespace SimpleFlag.Core;

/// <summary>
/// This interface defines the contract for the SimpleFlagDataSourceMigration.
/// </summary>
public interface ISimpleFlagDataSourceMigration
{
    /// <summary>
    /// The options for the migration.
    /// </summary>
    SimpleFlagMigrationOptions SimpleFlagMigrationOptions { get; set; }

    /// <summary>
    /// Run the migration.
    /// </summary>
    void Run();
}
