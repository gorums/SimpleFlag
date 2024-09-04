namespace SimpleFlag.Core;

/// <summary>
/// This exception is thrown when a flag does not exist.
/// </summary>
internal class SimpleFlagDoesNotExistException : Exception
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

/// <summary>
/// This exception is thrown when a user does not exist in the feature flag segments.
/// </summary>
internal class SimpleFlagUserDoesNotExistInSegmentException : Exception
{
    /// <summary>
    /// Initializes a new instance of the SimpleFlagUserDoesNotExistInSegmentException.
    /// </summary>
    /// <param name="message">The error message</param>
    public SimpleFlagUserDoesNotExistInSegmentException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the SimpleFlagUserDoesNotExistInSegmentException.
    /// </summary>
    /// <param name="message">The error message</param>
    /// <param name="innerException">The inner exception</param>
    public SimpleFlagUserDoesNotExistInSegmentException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

/// <summary>
/// This exception is thrown when a flag does not exist.
/// </summary>
public class SimpleFlagExistException : Exception
{
    /// <summary>
    /// Initializes a new instance of the SimpleFlagDoesNotExistException.
    /// </summary>
    /// <param name="message">The error message</param>
    public SimpleFlagExistException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the SimpleFlagDoesNotExistException.
    /// </summary>
    /// <param name="message">The error message</param>
    /// <param name="innerException">The inner exception</param>
    public SimpleFlagExistException(string message, Exception innerException) : base(message, innerException)
    {
    }
}