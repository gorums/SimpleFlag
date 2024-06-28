using SimpleFlag.Core;
using SimpleFlag.Core.DataSource.Internal;
using SimpleFlag.Core.Models;

namespace SimpleFlag;

/// <summary>
/// This class is the implementation of the ISimpleFlagService.
/// </summary>
internal class SimpleFlagClient : ISimpleFlagClient
{
    private readonly SimpleFlagDataSource _simpleFlagDataSource;
    private readonly SimpleFlagOptions _simpleFlagOptions;

    private string _domain;

    /// <summary>
    /// Initializes a new instance of the SimpleFlagService.
    /// </summary>
    /// <param name="simpleFlagDataSource"><see cref="ISimpleFlagDataSource"/></param>
    /// <param name="simpleFlagOptions"><see cref="SimpleFlagOptions"/></param>
    public SimpleFlagClient(SimpleFlagDataSource simpleFlagDataSource, SimpleFlagOptions simpleFlagOptions)
    {
        _simpleFlagDataSource = simpleFlagDataSource;
        _simpleFlagOptions = simpleFlagOptions;

        _domain = _simpleFlagOptions.Domain;
    }

    /// <inheritdoc />
    public ISimpleFlagClient Domain(string domain)
    {
        _domain = domain;
        return this;
    }

    /// <inheritdoc />
    public Task<bool?> EvaluateAsync(string flag, FeatureFlagUser? user = null, bool? defaultValue = null, CancellationToken cancellationToken = default)
    {
        //return _simpleFlagDataSource.EvaluateAsync(flag, cancellationToken);
        throw new NotImplementedException();
    }

    public Task<int?> EvaluateAsync(string flag, FeatureFlagUser? user = null, int? defaultValue = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<double?> EvaluateAsync(string flag, FeatureFlagUser? user = null, double? defaultValue = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<string?> EvaluateAsync(string flag, FeatureFlagUser? user = null, string? defaultValue = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
