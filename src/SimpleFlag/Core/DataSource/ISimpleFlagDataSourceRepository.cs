namespace SimpleFlag.Core.DataSource;

/// <summary>
/// This interface defines the contract for the SimpleFlagDataSourceRepository.
/// </summary>
public interface ISimpleFlagDataSourceRepository
{
    /// <summary>
    /// The options for the repository.
    /// </summary>
    SimpleFlagRepositoryOptions SimpleFlagRepositoryOptions { get; set; }

    /// <summary>
    /// Obtains the value of the flag.
    /// </summary>
    /// <param name="flag">The flag</param>
    /// <param name="cancellation">The cancellation token</param>
    /// <returns>The value of the flag</returns>
    /// <exception cref="SimpleFlagDoesNotExistException">Thrown when the flag does not exist</exception>
    Task<string> GetFlagValueAsync(string flag, CancellationToken cancellation = default);
}
