using FluentAssertions;
using Moq;
using SimpleFlag.Core;
using SimpleFlag.Core.DataSource;

namespace SimpleFlag.UnitTests;

public class SimpleFlagServiceTests
{
    private readonly Mock<ISimpleFlagDataSource> _mockDataSource;
    private readonly SimpleFlagService _simpleFlagService;

    public SimpleFlagServiceTests()
    {
        _mockDataSource = new Mock<ISimpleFlagDataSource>();
        _simpleFlagService = new SimpleFlagService(_mockDataSource.Object, new SimpleFlagOptions());
    }

    [Fact]
    public async Task EvaluateAsync_ShouldCallDataSourceEvaluateAsync()
    {
        // Arrange
        string flag = "feature-flag";
        CancellationToken cancellationToken = default;

        // Act
        await _simpleFlagService.EvaluateAsync(flag, cancellationToken);

        // Assert
        _mockDataSource.Verify(ds => ds.EvaluateAsync(flag, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task TryEvaluateAsync_WhenFlagExists_ShouldReturnTrueAndResult()
    {
        // Arrange
        string flag = "feature-flag";
        CancellationToken cancellationToken = default;
        bool expectedResult = true;

        _mockDataSource.Setup(ds => ds.EvaluateAsync(flag, cancellationToken)).ReturnsAsync(expectedResult);

        // Act
        var (success, result) = await _simpleFlagService.TryEvaluateAsync(flag, cancellationToken);

        // Assert
        success.Should().BeTrue();
        expectedResult.Should().Be(result);
    }

    [Fact]
    public async Task TryEvaluateAsync_WhenFlagDoesNotExist_ShouldReturnFalseAndFalseResult()
    {
        // Arrange
        string flag = "non-existent-flag";
        CancellationToken cancellationToken = default;

        _mockDataSource.Setup(ds => ds.EvaluateAsync(flag, cancellationToken)).ThrowsAsync(new SimpleFlagDoesNotExistException(""));

        // Act
        var (success, result) = await _simpleFlagService.TryEvaluateAsync(flag, cancellationToken);

        // Assert
        success.Should().BeFalse();
        result.Should().BeFalse();
    }
}
