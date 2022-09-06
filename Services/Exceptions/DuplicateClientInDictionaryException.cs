namespace Services.Exceptions;

public class DuplicateClientInDictionaryException : Exception
{
    public DuplicateClientInDictionaryException(string message) : base(message) 
    { 

    }
}

