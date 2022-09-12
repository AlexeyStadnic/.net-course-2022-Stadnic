using Models;

namespace Services;

public interface IEmployeeStorage : IStorage<Employee>
{
    public List<Employee> Data { get; }
}