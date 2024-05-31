using System.Security.Cryptography;
using System.Text;

namespace SimpleFlag.Core.Models;
public static class FeatureFlagRollout
{
    public static bool ShouldEvaluateFeature(FeatureFlagVariant featureFlagVariant, string featureName, string userId = null)
    {
        var random = new Random();
        // If no user ID is provided, treat it as a generic rollout
        if (string.IsNullOrEmpty(userId))
        {
            var rolloutPercentage = random.NextDouble() * 100;
            return rolloutPercentage < featureFlagVariant.RolloutPercentage;
        }

        // Hash the user ID and feature name to get a stable value
        int hash = CalculateHash(userId, featureName);

        // Map the hash to a value between 0 and 100
        double rolloutValue = (hash % 100) + 1;

        // Evaluate if the rollout value falls within the rollout percentage
        return rolloutValue <= featureFlagVariant.RolloutPercentage;
    }

    private static int CalculateHash(string userId, string featureName)
    {
        // Use a stable hashing algorithm (e.g., SHA256) to ensure consistent results
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes($"{userId}:{featureName}"));
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}