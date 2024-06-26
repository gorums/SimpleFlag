using FluentAssertions;
using SimpleFlag.Core.Models;

namespace SimpleFlag.UnitTests
{
    public class FeatureFlagRuleTests
    {
        [Fact]
        public void Evaluate_ShouldReturnTrue_WhenRuleEvaluatesToTrueWithUser()
        {
            // Arrange
            var rule = "name == \"Test\" and description == \"Desc Test\"";
            var value = new FeatureFlagVariant(FeatureFlagTypes.String, "enabled");

            var payload = new User
            {
                Name = "Test",
                Description = "Desc Test"
            };

            var featureFlagRule = new FeatureFlagRule(rule, value);

            // Act
            var result = featureFlagRule.Evaluate(payload);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Evaluate_ShouldReturnTrue_WhenRuleEvaluatesToFalseWithUser()
        {
            // Arrange
            var rule = "Name == \"Test\"";
            var value = new FeatureFlagVariant(FeatureFlagTypes.String, "enabled");

            var payload = new User
            {
                Name = "no_Test",
                Description = "No Test"
            };

            var featureFlagRule = new FeatureFlagRule(rule, value);

            // Act 
            var result = featureFlagRule.Evaluate(payload);

            // Assert
            result.Should().BeFalse();
        }
    }

    internal class User : FeatureFlagUser
    {
        Guid FeatureFlagUser.Id => Guid.NewGuid();

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
