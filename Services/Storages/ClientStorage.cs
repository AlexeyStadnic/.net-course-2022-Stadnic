namespace Services;
using Models;
using Exceptions;
public class ClientStorage : IClientStorage
{
    private IClientStorage _clientStorage;
    public Dictionary<Client, List<Account>> Data { get; }
    public ClientStorage()
    {
        Data = new Dictionary<Client, List<Account>>();
    }
    public void Add(Client client)
    {
        var accountsDefaultList = new List<Account>();
        var accountDefault = new Account();

        var currencyUSD = new Currency();
        currencyUSD.Name = "USD";
        currencyUSD.Code = 840;

        accountDefault.Amount = 900;
        accountDefault.Currency = currencyUSD;

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
            && (a.Currency.Name == account.Currency.Name)).Amount = account.Amount;
    }
    public void DeleteAccount(Client client,Account account)
    {       
               
    }
}