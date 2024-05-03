using SimpleFlag.Core.DataSource;

namespace DemoApi.Postgresql;

public class MyDataSourceRepository : ISimpleFlagDataSourceRepository
{
    private static readonly Lazy<ISimpleFlagDataSourceRepository> _dataSourceRepository = new Lazy<ISimpleFlagDataSourceRepository>(() => new MyDataSourceRepository());

    public SimpleFlagRepositoryOptions SimpleFlagRepositoryOptions { get; set; } = new SimpleFlagRepositoryOptions();

    private MyDataSourceRepository()
    {
    }

    public static ISimpleFlagDataSourceRepository Instance => _dataSourceRepository.Value;

    public Task<string> GetFlagValueAsync(string flag, CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }
}
