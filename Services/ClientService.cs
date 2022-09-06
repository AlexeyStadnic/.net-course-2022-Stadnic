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

        dictionaryClients.Add(client, accountsDefaultList);
    }
}