namespace Services.Exceptions;

public class NoPassportException : Exception
{
    public NoPassportException(string message) : base(message)
    {
        
    }
}