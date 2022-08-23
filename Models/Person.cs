namespace Models;

public class Person
{
    public string Name { get; set; }
    public int Passport { get; set; }
    public string Phone { get; set; }
    public DateTime Birthday { get; set; }

    public Person(string name, int passport, string phone, DateTime birthday)
    {
        Name = name;
        Passport = passport;
        Phone = phone;
        Birthday = birthday;
    }

    public Person()
    {
    }
}