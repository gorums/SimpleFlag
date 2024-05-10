namespace SimpleFlag.Core;

/// <summary>
/// This exception is thrown when a flag does not exist.
/// </summary>
public class SimpleFlagDoesNotExistException : Exception
{
    /// <summary>
    /// Initializes a new instance of the SimpleFlagDoesNotExistException.
    /// </summary>
    /// <param name="message">The error message</param>
    public SimpleFlagDoesNotExistException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the SimpleFlagDoesNotExistException.
    /// </summary>
    /// <param name="message">The error message</param>
    /// <param name="innerException">The inner exception</param>
    public SimpleFlagDoesNotExistException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
