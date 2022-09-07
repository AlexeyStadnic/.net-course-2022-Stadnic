using Models;
using Services.Exceptions;

namespace Services;

public class EmployeeService
{
    public readonly List<Employee> employees = new List<Employee>();

    public void AddEmployee(Employee employee) 
    {
        var today = DateTime.Today;
        
        if (today.Year - employee.Birthday.Year < 18)
        {
            throw new YoungAgeException("Ошибка. Клиент слишком молод.");
        }
        
        if (employee.Passport == 0)
        {
            throw new NoPassportException("Ошибка. У клиента отсутствует пасспорт.");
        }
        
        employees.Add(employee);
    }
}