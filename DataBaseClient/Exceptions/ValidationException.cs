namespace DataBaseClient.Exceptions;

internal class ValidationException : Exception
{
    public string ValidationMessage { get; private set; }

    public ValidationException(string message)
    {
        ValidationMessage = message;
    }
}
