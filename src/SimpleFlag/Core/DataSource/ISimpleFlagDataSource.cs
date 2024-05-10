
namespace SimpleFlag.Core.DataSource;

/// <summary>
/// This interface defines the contract for the SimpleFlagDataSource.
/// </summary>
public interface ISimpleFlagDataSource
{
    /// <summary>
    /// Evaluates the flag and returns the result.
    /// </summary>
    /// <param name="flag"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> EvaluateAsync(string flag, CancellationToken cancellationToken);

    /// <summary>
    /// Run the migration.
    /// </summary>
    void RunMigration();
}
