namespace Models;

public class Client:Person
{
    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;
        if (!(obj is Client))
            return false;
        var client = (Client)obj;
                
        return client.Name == Name && client.Passport == Passport && client.Phone == Phone && client.Birthday == Birthday;
    }

    public override int GetHashCode() 
    {
        return Name.GetHashCode() ^ Passport.GetHashCode() ^ Phone.GetHashCode();
    }
}

