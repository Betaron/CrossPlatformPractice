namespace MealyStateMachine.Exceptions;

public class ValidationException : Exception
{
    public string ValidationMessage { get; private set; }

    public ValidationException(string message)
    {
        ValidationMessage = message;
    }
}
