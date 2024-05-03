
namespace SimpleFlag.Core.DataSource;
public interface ISimpleFlagDataSource
{
    Task<bool> EvaluateAsync(string flag, CancellationToken cancellationToken);
    void RunMigration();
}
