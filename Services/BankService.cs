using Models;
namespace Services;

public class BankService
{
  
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
    
}