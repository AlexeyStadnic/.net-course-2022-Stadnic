using ModelsDB;
using Services.Storages;

namespace Services;
public class EmployeeStorage : IEmployeeStorage
{
    public BankContext Data { get; }

    public EmployeeStorage()
    {
        Data = new BankContext();
    }
    
    public void Add(EmployeeDB employee)
    {
        Data.Employees.Add(employee);
        Data.SaveChanges();
    }

    public EmployeeDB Get(Guid id)
    {
        return Data.Employees.FirstOrDefault(x => x.Id == id);
    }
    public void Delete(EmployeeDB employee)
    {
        Data.Employees.Remove(employee);
        Data.SaveChanges();
    }

    public void Update(EmployeeDB employee)
    {
        Data.Employees.Update(employee);
        Data.SaveChanges();
    }
}