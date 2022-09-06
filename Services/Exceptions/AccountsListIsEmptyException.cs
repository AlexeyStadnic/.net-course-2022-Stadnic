namespace Services.Exceptions
{
    public class AccountsListIsEmptyException : Exception
    {
        public AccountsListIsEmptyException(string message) : base(message) 
        { 

        }
    }
}
