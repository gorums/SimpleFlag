using Moq;
using SimpleFlag.Core;
using SimpleFlag.Core.Internal;

namespace SimpleFlag.UnitTests;

public class SimpleFlagClientTests
{
    private readonly Mock<ISimpleFlagDataSourceRepository> _mockSimpleFlagDataSourceRepository;
    private readonly Mock<ISimpleFlagDataSourceMigration> _mockSimpleSimpleFlagDataSourceMigration;
    private readonly ISimpleFlagClient _simpleFlagService;

    public SimpleFlagClientTests()
    {
        _mockSimpleFlagDataSourceRepository = new Mock<ISimpleFlagDataSourceRepository>();
        _mockSimpleSimpleFlagDataSourceMigration = new Mock<ISimpleFlagDataSourceMigration>();

        var simpleFlagDataSource = new SimpleFlagDataSource(new SimpleFlagDataSourceOptions
        {
            ConnectionString = "connection-string",
            DataSourceRepository = _mockSimpleFlagDataSourceRepository.Object,
            DataSourceMigration = _mockSimpleSimpleFlagDataSourceMigration.Object
        });

        _simpleFlagService = new SimpleFlagClient(null, null, simpleFlagDataSource);
    }
}
