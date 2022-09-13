using Models;
namespace Services;

public class BankService
{
    public List<Person> BlackList = new List<Person>();
    public int PayrollForBankOwners(decimal profit, decimal expenses, int numberOfOwners)
    {
        return (int)(profit - expenses) / numberOfOwners;
    }

    public Employee ConversionClientToEmployee(Client client)
    {
        Employee employee = new Employee();
        employee.Name = client.Name;
        employee.Birthday = client.Birthday;
        employee.Passport = client.Passport;
        employee.Phone = client.Phone;
            
        return employee;
    }

    public void AddBonus<T>(T person) where T : Person
    {
        person.Bonus++;
    }

    public void AddToBlackList<T>(T person) where T : Person
    {
        if (!BlackList.Contains(person))
         BlackList.Add(person); 
    }

    public bool IsPersonInBlackList<T>(T person) where T : Person
    {
        return BlackList.Contains(person);
    }
}