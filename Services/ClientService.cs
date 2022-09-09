using Models;
using Services.Exceptions;

namespace Services;

public class ClientService
{
    private ClientStorage _clientStorage;
    public ClientService(ClientStorage clientStorage)
    {
        _clientStorage = clientStorage;
    }
    public void AddClient(Client client)
    {
        if (DateTime.Today.Year - client.Birthday.Year < 18)
        {
            throw new YoungAgeException("Ошибка. Клиент слишком молод.");
        }

        if (client.Passport == 0)
        {
            throw new NoPassportException("Ошибка. У клиента отсутствует пасспорт.");
        }
        _clientStorage.Add(client);
    }
    
    public void AddAccount(Client client,Account account)
    {       
        var accountsOfClient = _clientStorage._dictionaryClients[client];
        if (accountsOfClient.Contains(account))
        {
            throw new AccountAlreadyExistsException("Ошибка. У клиента уже открыт такой счет.");
        }
        _clientStorage.Add(client,account);
    }

    /*public void EditAccount(Client client, Account account)
    {
        var accountsOfClient = _clientStorage._dictionaryClients[client];
        int numberOfChanges = 0;

        for (int i = 0; i < accountsOfClient.Count; i++)
        {
            if ((accountsOfClient[i].Currency.Code == account.Currency.Code) &&
                    (accountsOfClient[i].Currency.Name == account.Currency.Name))
            {
                accountsOfClient[i].Amount = account.Amount;
                numberOfChanges++;
            }
        }
        if (numberOfChanges == 0)
        {
            throw new NoSuchAccountException("Ошибка. У клиента нет такого счета");
        }        
    }*/
}