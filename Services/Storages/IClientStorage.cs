using Models;

namespace Services.Storages;
public interface IClientStorage : IStorage<Client>
{    
    public BankContext Data { get; }
    void AddAccount(Client id, Account account);
    Client Get(Guid id);
    void DeleteAccount(Client client, Account account);
    void UpdateAccount(Client client, Account account);
}