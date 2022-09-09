using Models;
using Services.Exceptions;

namespace Services;

public class EmployeeService
{
    private EmployeeStorage _employeeStorage;
    public EmployeeService(EmployeeStorage employeeStorage)
    {
        _employeeStorage = employeeStorage;
    }

    public void AddEmployee(Employee employee) 
    {       
        if (DateTime.Today.Year - employee.Birthday.Year < 18)
        {
            throw new YoungAgeException("Ошибка. Клиент слишком молод.");
        }
        
        if (employee.Passport == 0)
        {
            throw new NoPassportException("Ошибка. У клиента отсутствует пасспорт.");
        }
        
        _employeeStorage.Add(employee);
    }

    public List<Employee> GetEmployees()
    {
        return _employeeStorage._employees; 
    }
}