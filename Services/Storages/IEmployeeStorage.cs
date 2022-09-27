using Models;

namespace Services.Storages;
public interface IEmployeeStorage : IStorage<Employee>
{
    public BankContext Data { get; }
}