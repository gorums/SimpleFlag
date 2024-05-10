using FluentAssertions;
using Moq;
using SimpleFlag.Core;
using SimpleFlag.Core.DataSource;

namespace SimpleFlag.UnitTests;

public class SimpleFlagServiceTests
{
    private readonly Mock<ISimpleFlagDataSourceRepository> _mockSimpleFlagDataSourceRepository;
    private readonly Mock<ISimpleFlagDataSourceMigration> _mockSimpleSimpleFlagDataSourceMigration;
    private readonly ISimpleFlagService _simpleFlagService;

    public SimpleFlagServiceTests()
    {
        _mockSimpleFlagDataSourceRepository = new Mock<ISimpleFlagDataSourceRepository>();
        _mockSimpleSimpleFlagDataSourceMigration = new Mock<ISimpleFlagDataSourceMigration>();

        var simpleFlagDataSource = new SimpleFlagDataSource(new SimpleFlagDataSourceOptions
        {
            ConnectionString = "connection-string",
            DataSourceRepository = _mockSimpleFlagDataSourceRepository.Object,
            DataSourceMigration = _mockSimpleSimpleFlagDataSourceMigration.Object
        });

        _simpleFlagService = new SimpleFlagService(simpleFlagDataSource, new SimpleFlagOptions());
    }

    [Theory]
    [InlineData("true", true)]
    [InlineData("false", false)]
    public async Task TryEvaluateAsync_WhenFlagExists_ShouldReturnSuccessAndResult(string flagResult, bool evaluation)
    {
        // Arrange
        string flag = "feature-flag";
        CancellationToken cancellationToken = default;

        _mockSimpleFlagDataSourceRepository.Setup(ds => ds.GetFlagValueAsync(flag, cancellationToken)).ReturnsAsync(flagResult);

        // Act
        var (success, result) = await _simpleFlagService.TryEvaluateAsync(flag, cancellationToken);

        // Assert
        _mockSimpleFlagDataSourceRepository.Verify(ds => ds.GetFlagValueAsync(flag, cancellationToken), Times.Once);
        success.Should().BeTrue();
        evaluation.Should().Be(result);
    }

    [Fact]
    public async Task TryEvaluateAsync_WhenFlagDoesNotExist_ShouldReturnFalseAndFalseResult()
    {
        // Arrange
        string flag = "non-existent-flag";
        CancellationToken cancellationToken = default;

        _mockSimpleFlagDataSourceRepository.Setup(ds => ds.GetFlagValueAsync(flag, cancellationToken)).ThrowsAsync(new SimpleFlagDoesNotExistException(""));

        // Act
        var (success, result) = await _simpleFlagService.TryEvaluateAsync(flag, cancellationToken);

        // Assert
        success.Should().BeFalse();
        result.Should().BeFalse();
    }
}
