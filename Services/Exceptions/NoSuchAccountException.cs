namespace Services.Exceptions;

public class NoSuchAccountException : Exception
{    
    public NoSuchAccountException(string message) : base(message) 
    { 

    }
}

