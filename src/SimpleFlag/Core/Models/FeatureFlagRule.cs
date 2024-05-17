using System.Linq.Dynamic.Core;

namespace SimpleFlag.Core.Models
{
    public class FeatureFlagRule
    {
        public string Rule { get; init; }

        public FeatureFlagVariant Value { get; init; }

        // percentage rollout values 20% easy 30% medium 50% hard

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
