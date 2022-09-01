namespace Models;

public class Employee:Person
{
    public string Contract { get; set; }   
    public int Salary { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        if (!(obj is Employee))
            return false;
        var employee = (Employee)obj;
                
        return employee.Name == Name && employee.Passport == Passport && employee.Phone == Phone && 
               employee.Birthday == Birthday && employee.Contract == Contract && employee.Salary == Salary;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode() ^ Passport.GetHashCode() ^ Salary.GetHashCode();
    }
}