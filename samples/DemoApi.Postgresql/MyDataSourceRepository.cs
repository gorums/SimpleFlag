using SimpleFlag.Core;
using SimpleFlag.Core.Entities;

namespace DemoApi.Postgresql;

public class MyDataSourceRepository : ISimpleFlagDataSourceRepository
{
    private static readonly Lazy<ISimpleFlagDataSourceRepository> _dataSourceRepository = new Lazy<ISimpleFlagDataSourceRepository>(() => new MyDataSourceRepository());

    public SimpleFlagRepositoryOptions SimpleFlagRepositoryOptions { get; set; } = new SimpleFlagRepositoryOptions();

    private MyDataSourceRepository()
    {
    }

    public static ISimpleFlagDataSourceRepository Instance => _dataSourceRepository.Value;

    public Task<FeatureFlag> GetFeatureFlagAsync(string domain, string flag, FeatureFlagUser? user, CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }

    public Task<FeatureFlag> AddFeatureFlagAsync(string domain, FeatureFlag featureFlag, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
