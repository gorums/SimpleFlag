namespace SimpleFlag.Core;
public interface ISimpleFlagService
{
    Task<bool> EvaluateAsync(string flag, CancellationToken cancellationToken = default);

    Task<(bool success, bool result)> TryEvaluateAsync(string flag, CancellationToken cancellationToken = default);
}
