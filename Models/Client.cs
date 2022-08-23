namespace Models;

public class Client:Person
{
    public Client(string name, int passport, string phone, DateTime birthday) : base(name, passport, phone, birthday)
    {
    }

    public Client()
    {
    }
}