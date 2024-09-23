using SimpleFlag;
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

    public Task<IEnumerable<FeatureFlag>> GetFeatureFlagsAsync(string domain, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<FeatureFlag> UpdateFeatureFlagAsync(string domain, FeatureFlag featureFlag, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteFeatureFlagAsync(Guid flagId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FeatureFlagSegment>> GetSegmentsAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<FeatureFlagSegment> AddSegmentAsync(FeatureFlagSegment segment, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<FeatureFlagSegment> UpdateSegmentAsync(FeatureFlagSegment segment, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteSegmentAsync(Guid segmentId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FeatureFlagUser>> GetUsersAsync(string? segment, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FeatureFlagUser>> AddUsersAsync(List<SimpleFlagUser> users, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<SimpleFlagUser>> UpdateUsersAsync(List<SimpleFlagUser> users, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUsersAsync(List<Guid> userIds, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AddSegmentToFeatureFlagAsync(string segment, Guid flagId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUsersFromSegmentAsync(string segment, List<Guid> userIds, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AddUsersToSegmentAsync(List<SimpleFlagUser> users, string segment, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task CleanUsersOnSegmentAsync(string segment, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FeatureFlagDomain>> GetDomainsAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
