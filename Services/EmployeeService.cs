using ModelsDB;
using Services.Exceptions;

namespace Services;

public class EmployeeService
{
    private EmployeeStorage _employeeStorage;
    public EmployeeService(EmployeeStorage employeeStorage)
    {
        _employeeStorage = employeeStorage;
    }

    public EmployeeDB Get(Guid id)
    {
        return _employeeStorage.Get(id);
    }

    public void Add(EmployeeDB employee) 
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

    public void Update(EmployeeDB employee)
    {
        var oldEmployee = Get(employee.Id);
        if (oldEmployee != null)
        {
            oldEmployee.Name = employee.Name;
            oldEmployee.Phone = employee.Phone;
            oldEmployee.Birthday = employee.Birthday;
            oldEmployee.Bonus = employee.Bonus;
            _employeeStorage.Update(oldEmployee);
        }
    }

    public void Delete(EmployeeDB employee)
    {
        var oldEmployee = Get(employee.Id);
        if (oldEmployee != null)
        {
            _employeeStorage.Delete(oldEmployee);
        }
    }

    public List<EmployeeDB> GetEmployees(Filter filter)
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

        return selection.ToList();
    }
}