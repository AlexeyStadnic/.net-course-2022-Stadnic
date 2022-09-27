using ModelsDb;
using Models;
using Services.Storages;

namespace Services;
public class EmployeeStorage : IEmployeeStorage

{
    public BankContext Data { get; }

    public EmployeeStorage()
    {
        Data = new BankContext();
    }
    
    public void Add(Employee employee)
    {
        var employeeDb = new EmployeeDb();
        employeeDb.Name = employee.Name;
        employeeDb.Phone = employee.Phone;
        employeeDb.Birthday = employee.Birthday;
        employeeDb.Birthday = DateTime.SpecifyKind(employee.Birthday, DateTimeKind.Utc);
        employeeDb.Id = Guid.NewGuid();
        employeeDb.Bonus = employee.Bonus;
        employeeDb.Passport = employee.Passport;
        employeeDb.Salary = employee.Salary;
        employeeDb.Contract = employee.Contract;
        Data.Employees.Add(employeeDb);
        Data.SaveChanges();
    }

    public Employee Get(Guid id)
    {
        var employeeDb = Data.Employees.FirstOrDefault(e => e.Id == id);
        var employee = new Employee();
        if (employeeDb != null)
        {
            employee.Phone = employeeDb.Phone;
            employee.Birthday = employeeDb.Birthday;
            employee.Name = employeeDb.Name;
            employee.Passport = employeeDb.Passport;
            employee.Bonus = employeeDb.Bonus;
            employee.Contract = employeeDb.Contract;
            employee.Salary = employeeDb.Salary;
        }
        return employee;
    }
    
    public void Delete(Employee employee)
    {
        var oldEmployee = Data.Employees.FirstOrDefault(c => c.Phone.Equals(employee.Phone));
        if (oldEmployee != null)
        {
            Data.Employees.Remove(oldEmployee);
            Data.SaveChanges();
        }
    }

    public void Update(Employee employee)
    {
        var oldEmployee = Data.Employees.FirstOrDefault(c => c.Passport == employee.Passport);
        if (oldEmployee != null)
        {
            oldEmployee.Name = employee.Name;
            oldEmployee.Phone = employee.Phone;
            oldEmployee.Birthday = employee.Birthday;
            oldEmployee.Bonus = employee.Bonus;
            oldEmployee.Contract = employee.Contract;
            oldEmployee.Salary = employee.Salary;
            Data.Employees.Update(oldEmployee);
        }
        Data.SaveChanges();
    }
}