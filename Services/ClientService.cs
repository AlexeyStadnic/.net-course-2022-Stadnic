using Bogus.Bson;
using Models;
using Services.Exceptions;

namespace Services;

public class ClientService
{
    public readonly Dictionary<Client, List<Account>> _dictionaryClients = new Dictionary<Client, List<Account>>();

    public void AddClient(Client client)
    {
        var accountsDefaultList = new List<Account>();
        var accountDefault = new Account();

        var currencyUSD = new Currency();
        currencyUSD.Name = "USD";
        currencyUSD.Code = 840;

        accountDefault.Amount = 12125;
        accountDefault.Currency = currencyUSD;

        accountsDefaultList.Add(accountDefault);        

        if (DateTime.Today.Year - client.Birthday.Year < 18)
        {
            throw new YoungAgeException("Ошибка. Клиент слишком молод.");
        }

        if (client.Passport == 0)
        {
            throw new NoPassportException("Ошибка. У клиента отсутствует пасспорт.");
        }        

        _dictionaryClients.Add(client, accountsDefaultList);
    }
    
    public void AddAccount(Client client,Account account)
    {       
        var accountsOfClient = _dictionaryClients[client];
        if (accountsOfClient.Contains(account))
        {
            throw new AccountAlreadyExistsException("Ошибка. У клиента уже открыт такой счет.");
        }
        
        _dictionaryClients[client].Add(account);        
    }
    
    public void EditAccount(Client client,Account account)
    {        
        var accountsOfClient = _dictionaryClients[client];

        var currentAccount = accountsOfClient.Find(a => a.Currency.Equals(account.Currency));
        if (currentAccount != null)
        {
            var index = accountsOfClient.IndexOf(currentAccount);
            accountsOfClient[index].Amount = account.Amount;
        }
        else throw new NoSuchAccountException("Ошибка. У клиента нет такого счета");    
    }
}