using Models;
using Services.Exceptions;

namespace Services;

public class ClientService
{
    public readonly Dictionary<Client, List<Account>> dictionaryClients = new Dictionary<Client, List<Account>>();

    public void AddClientInDictionary(Client client)
    {
        var accountsDefaultList = new List<Account>();
        var accountDefault = new Account();

        var currencyUSD = new Currency();
        currencyUSD.Name = "USD";
        currencyUSD.Code = 840;

        accountDefault.Amount = 12125;
        accountDefault.Currency = currencyUSD;

        accountsDefaultList.Add(accountDefault);        

        var today = DateTime.Today;

        if (today.Year - client.Birthday.Year < 18)
        {
            throw new YongAgeException("Ошибка. Клиент слишком молод.");
        }

        if (client.Passport == 0)
        {
            throw new NoPassportException("Ошибка. У клиента отсутствует пасспорт.");
        }

        if (accountsDefaultList.Count == 0)
        {
            throw new AccountsListIsEmptyException("Ошибка. У клиента отсутствуют счета.");
        }               

        dictionaryClients.Add(client, accountsDefaultList);
    }
    
    public void AddAccountInClient(Client client,Account account)
    {
        try
        {
            var accountsOfClient = dictionaryClients[client];
            if (accountsOfClient.Contains(account))
            {
                throw new AccountAlreadyExistsException("Ошибка. У клиента уже открыт такой счет.");
            }
            accountsOfClient.Add(account);
            dictionaryClients[client] = accountsOfClient;
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine(e);
        }
    }
    
    public void EditAccountInClient(Client client,Account currentAccount, Account newAccount)
    {
        try
        {
            var accountsOfClient = dictionaryClients[client];
            accountsOfClient[accountsOfClient.IndexOf(currentAccount)] = newAccount;
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine(e);
        }
    }
}