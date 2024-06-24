using System.Linq.Dynamic.Core;

namespace SimpleFlag.Core.Models
{
    public class FeatureFlagRule
    {
        public string Rule { get; init; }

        public FeatureFlagSegment? IncludeSegment { get; init; }

        public FeatureFlagSegment? ExcludeSegment { get; init; }

        public FeatureFlagVariant Value { get; init; }

        public FeatureFlagRule(string rule, FeatureFlagVariant value)
        {
            Rule = rule;
            Value = value;
        }

        public bool Evaluate<T>(T payload) where T : FeatureFlagUser
        {
            var config = new ParsingConfig { AllowNewToEvaluateAnyType = true };
            var lambda = DynamicExpressionParser.ParseLambda(config, typeof(T), typeof(bool), Rule);

            // Compile and execute the lambda expression
            var func = (Func<T, bool>)lambda.Compile();

            return func(payload);
        }
    }
}
