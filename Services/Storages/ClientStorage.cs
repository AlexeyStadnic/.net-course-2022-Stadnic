using Models;
namespace Services.Storages;
public class ClientStorage : IClientStorage
{
    public Dictionary<Client, List<Account>> Data { get; }
    public ClientStorage()
    {
        Data = new Dictionary<Client, List<Account>>();
    }
    public void Add(Client client)
    {
        var accountsDefaultList = new List<Account>();
        var accountDefault = new Account();

        var currencyUsd = new Currency();
        currencyUsd.Name = "USD";
        currencyUsd.Code = 840;

        accountDefault.Amount = 900;
        accountDefault.Currency = currencyUsd;

        accountsDefaultList.Add(accountDefault);
        Data.Add(client, accountsDefaultList);
    }
    public void Delete(Client client)
    {
        
    }
    public void Update(Client client)
    {
        
    }
    public void AddAccount(Client client,Account account)
    {       
        Data[client].Add(account);        
    }
    public void UpdateAccount(Client client, Account account)
    {
        Data[client].FirstOrDefault(a => 
            (a.Currency.Code == account.Currency.Code) 
            && (a.Currency.Name == account.Currency.Name))!.Amount = account.Amount;
    }
    public void DeleteAccount(Client client,Account account)
    {       
               
    }
}