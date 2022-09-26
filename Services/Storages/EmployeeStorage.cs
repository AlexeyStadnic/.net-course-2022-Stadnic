using ModelsDb;
using Services.Storages;

namespace Services;
public class EmployeeStorage : IEmployeeStorage

{
    public BankContext Data { get; }

    public EmployeeStorage()
    {
        Data = new BankContext();
    }
    
    public void Add(EmployeeDb employee)
    {
        Data.Employees.Add(employee);
        Data.SaveChanges();
    }

    public EmployeeDb Get(Guid id)
    {
        return Data.Employees.FirstOrDefault(x => x.Id == id);
    }
    
    public void Delete(EmployeeDb employee)
    {
        Data.Employees.Remove(employee);
        Data.SaveChanges();
    }

    public void Update(EmployeeDb employee)
    {
        Data.Employees.Update(employee);
        Data.SaveChanges();
    }
}