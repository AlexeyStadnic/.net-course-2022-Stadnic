namespace Services;
using Models;
using Exceptions;
public class ClientStorage : IStorage
{
    public readonly Dictionary<Client, List<Account>> _dictionaryClients = new Dictionary<Client, List<Account>>();
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
        _dictionaryClients.Add(client, accountsDefaultList);
    }
    
    public void Add(Client client,Account account)
    {       
        _dictionaryClients[client].Add(account);        
    }
    
    public void Update(Client client, Account account)
    {
        _dictionaryClients[client].FirstOrDefault(a => 
            (a.Currency.Code == account.Currency.Code) 
            && (a.Currency.Name == account.Currency.Name)).Amount = account.Amount;
    }

    public void Add()
    {
        throw new NotImplementedException();
    }

    public void Delete()
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        throw new NotImplementedException();
    }
}