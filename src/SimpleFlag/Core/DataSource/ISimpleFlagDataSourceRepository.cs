namespace SimpleFlag.Core.DataSource;
public interface ISimpleFlagDataSourceRepository
{
    SimpleFlagRepositoryOptions SimpleFlagRepositoryOptions { get; set; }

    Task<string> GetFlagValueAsync(string flag, CancellationToken cancellation = default);
}
