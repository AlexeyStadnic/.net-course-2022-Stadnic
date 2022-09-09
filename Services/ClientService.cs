using Models;
using Services.Exceptions;
using System.Collections;

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

    public void UpdateAccount(Client client, Account account)
    {
        _clientStorage.Update(client, account);
    }

    public Dictionary<Client, List<Account>> GetClients()
    {
        return _clientStorage._dictionaryClients;       
    }

}