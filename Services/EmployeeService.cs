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

    public List<Employee> GetEmployees(Filter filter)
    {
        var selection = _employeeStorage.Data.
            Where(e => e.Birthday >= filter.DateFrom).
            Where(e => e.Birthday <= filter.DateBefore);

        if (filter.Name != null)
            selection = selection.Where(e => e.Name == filter.Name);
        
        if (filter.Phone != null)
            selection = selection.Where(e => e.Phone == filter.Phone);

        if (filter.Passport != 0)
            selection = selection.Where(e => e.Passport == filter.Passport);

        return selection.ToList();
    }
}