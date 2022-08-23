
using Models;

namespace BankService;
public class Bank
{
    public int PayrollForBankOwners(decimal profit, decimal expenses, int numberOfOwners)
    {
        return (int)(profit - expenses) / numberOfOwners;
    }

    public Employee ConversionClientToEmployee(Client client)
    {
        Person person = client;
        Employee employee = new Employee();
        if (person is Employee)
        {
            employee = (Employee)person;
            Console.WriteLine("Чудо. Превращение удалось!!!");
        }
        else
        {
            Console.WriteLine("Client не является объектом типа Employee.");
            Console.WriteLine("Поэтому придется скопировать все данные клиента в созданного нами сотрудника.");
            employee.Name = client.Name;
            employee.Birthday = client.Birthday;
            employee.Passport = client.Passport;
            employee.Phone = client.Phone;
        }
        return employee;
    }
}