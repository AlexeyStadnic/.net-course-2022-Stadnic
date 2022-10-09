namespace Models;

public class Account
{
	public Currency Currency { get; set; }
	public int Amount { get; set; }
    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;
        if (!(obj is Account))
            return false;
        var account = (Account)obj;

        return account.Amount == Amount && account.Currency.Equals(Currency);
    }    
}
