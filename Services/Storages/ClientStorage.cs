using ModelsDb;
namespace Services.Storages;
public class ClientStorage : IClientStorage
{
    public BankContext Data { get; }

    public ClientStorage()
    {
        Data = new BankContext();
    }

    public void Add(ClientDb client)
    {
        var defaultAccount = new AccountDb();
        defaultAccount.ClientId = client.Id;
        defaultAccount.Amount = 0;
        var currencys = Data.Currencys.ToList();
        defaultAccount.CurrencyId = currencys[0].Id;
        Data.Clients.Add(client);
        Data.Accounts.Add(defaultAccount);
        Data.SaveChanges();
    }

    public ClientDb Get(Guid id)
    {
        return Data.Clients.FirstOrDefault(x => x.Id == id);
    }

    public void Delete(ClientDb client)
    {
        Data.Clients.Remove(client);
        Data.SaveChanges();
    }

    public void Update(ClientDb client)
    {
        Data.Clients.Update(client);
        Data.SaveChanges();
    }

    public void AddAccount(Guid id, AccountDb account)
    {
        account.ClientId = id;
        Data.Accounts.Add(account);
        Data.SaveChanges();
    }
    
    public void UpdateAccount(Guid id, AccountDb account)
    {
        
    }

    public void DeleteAccount(Guid id, AccountDb account)
    {        
        Data.Accounts.Remove(account);
        Data.SaveChanges();
    }
}