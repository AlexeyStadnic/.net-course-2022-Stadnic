using ModelsDB;
using Models;
using Services.Exceptions;
using Services.Storages;

namespace Services;

public class ClientService
{
    private readonly IClientStorage _clientStorage;
    public ClientService(IClientStorage clientStorage)
    {
        _clientStorage = clientStorage;
    }
    public void Add(ClientDB client)
    {
        if (DateTime.Today.Year - client.Birthday.Year < 18)
        {
            throw new YoungAgeException("Ошибка. Клиент слишком молод.");
        }

        if (client.Passport == 0)
        {
            throw new NoPassportException("Ошибка. У клиента отсутствует паспорт.");
        }      

        _clientStorage.Add(client);
    }

    public ClientDB Get(Guid id)
    {        
        return _clientStorage.Get(id);
    }
    public void AddAccount(Guid id, AccountDB account)
    {        
        var accounts = _clientStorage.Data.Accounts.Where(x => x.CurrencyId == account.CurrencyId).ToList();
        if (accounts.Count == 0) _clientStorage.AddAccount(id, account);
        else throw new AccountAlreadyExistsException("Ошибка. У клиента уже открыт такой счет.");
    }

    /*public void UpdateAccount(Client client, Account account)
    {
        _clientStorage.UpdateAccount(client, account);
    }*/

    /*public Dictionary<Client, List<Account>> GetClients(Filter filter)
    {
        var selection = _clientStorage.Data.
            Where(c => c.Key.Birthday >= filter.DateFrom).
            Where(c => c.Key.Birthday <= filter.DateBefore);

        if (filter.Name != null)
            selection = selection.Where(c => c.Key.Name == filter.Name);

        if (filter.Phone != null)
            selection = selection.Where(c => c.Key.Phone == filter.Phone);

        if (filter.Passport != 0)
            selection = selection.Where(c => c.Key.Passport == filter.Passport);

        return selection.ToDictionary(c => c.Key, a => a.Value);
    }*/
}