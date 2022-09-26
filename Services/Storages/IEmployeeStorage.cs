using ModelsDB;

namespace Services.Storages;
public interface IEmployeeStorage : IStorage<EmployeeDB>
{
    public BankContext Data { get; }
}