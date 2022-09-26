using ModelsDb;

namespace Services.Storages;
public interface IClientStorage : IStorage<ClientDb>
{    
    public BankContext Data { get; }
    void AddAccount(Guid id, AccountDb account);
    ClientDb Get(Guid id);
    void DeleteAccount(Guid id, AccountDb account);    
}