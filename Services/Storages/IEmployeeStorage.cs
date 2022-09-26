using ModelsDb;

namespace Services.Storages;
public interface IEmployeeStorage : IStorage<EmployeeDb>
{
    public BankContext Data { get; }
}