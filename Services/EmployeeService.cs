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

    public Employee Get(Guid id)
    {
        return _employeeStorage.Get(id);
    }

    public void Add(Employee employee) 
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

    public void Update(Employee employee)
    {
        _employeeStorage.Update(employee);
    }

    public void Delete(Employee employee)
    {
        _employeeStorage.Delete(employee);
    }

    public List<Employee> GetEmployees(Filter filter)
    {
        var selection = _employeeStorage.Data.Employees.
            Where(e => e.Birthday >= filter.DateFrom).
            Where(e => e.Birthday <= filter.DateBefore);

        if (filter.Name != null)
            selection = selection.Where(e => e.Name == filter.Name);
        
        if (filter.Phone != null)
            selection = selection.Where(e => e.Phone == filter.Phone);

        if (filter.Passport != 0)
            selection = selection.Where(e => e.Passport == filter.Passport);

        var employeesDb = selection.ToList();
        var employees = new List<Employee>();

        foreach (var employeeDb in employeesDb)
        {
            var employee = new Employee();
            employee.Phone = employeeDb.Phone;
            employee.Name = employeeDb.Name;
            employee.Birthday = employeeDb.Birthday;
            employee.Contract = employeeDb.Contract;
            employee.Bonus = employeeDb.Bonus;
            employee.Passport = employeeDb.Passport;
            employee.Salary = employeeDb.Salary;

            employees.Add(employee);
        }
        return employees;
    }
}