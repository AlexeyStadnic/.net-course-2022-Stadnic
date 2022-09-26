using ModelsDB;
namespace Services.Storages;
public class ClientStorage : IClientStorage
{
    public BankContext Data { get; }
    public ClientStorage()
    {
        Data = new BankContext();
    }
    public void Add(ClientDB client)
    {
        var defaultAccount = new AccountDB();
        defaultAccount.ClientId = client.Id;
        defaultAccount.Amount = 0;
        var currencys = Data.Currencys.ToList();
        defaultAccount.CurrencyId = currencys[0].Id;
        Data.Clients.Add(client);
        Data.Accounts.Add(defaultAccount);
        Data.SaveChanges();
    }
    public ClientDB Get(Guid id)
    {
        return Data.Clients.FirstOrDefault(x => x.Id == id);
    }

    public void Delete(ClientDB client)
    {
        Data.Clients.Remove(client);
        Data.SaveChanges();
    }
    public void Update(ClientDB client)
    {
        Data.Clients.Update(client);
        Data.SaveChanges();
    }
    public void AddAccount(Guid id, AccountDB account)
    {
        account.ClientId = id;
        Data.Accounts.Add(account);
        Data.SaveChanges();
    }
    public void UpdateAccount(Guid id, AccountDB account)
    {
        
    }
    public void DeleteAccount(Guid id, AccountDB account)
    {        
        Data.Accounts.Remove(account);
        Data.SaveChanges();
    }
}