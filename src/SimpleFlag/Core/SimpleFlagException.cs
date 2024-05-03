namespace SimpleFlag.Core;
public class SimpleFlagDoesNotExistException : Exception
{
    public SimpleFlagDoesNotExistException(string message) : base(message)
    {
    }

    public SimpleFlagDoesNotExistException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
