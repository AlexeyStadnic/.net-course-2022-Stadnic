namespace Models;

public class Employee:Person
{
    public string Contract { get; set; }
    
    public int Salary { get; set; }
    public string Position { get; set; }
    public Employee(string name, int passport, string phone, DateTime birthday) : base(name, passport, phone, birthday)
    {
    }
    public Employee()
    {
    }
}