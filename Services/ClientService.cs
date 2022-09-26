using ModelsDb;
using Models;
using Services.Exceptions;
using Services.Storages;

namespace Services;

public class ClientService
{
    private IClientStorage _clientStorage;

    public ClientService(IClientStorage clientStorage)
    {
        _clientStorage = clientStorage;
    }

    public void Add(ClientDb client)
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

    public ClientDb Get(Guid id)
    {        
        return _clientStorage.Get(id);
    }
    public void AddAccount(Guid id, AccountDb account)
    {        
        var accounts = _clientStorage.Data.Accounts.Where(x => x.CurrencyId == account.CurrencyId).ToList();
        if (accounts.Count == 0) _clientStorage.AddAccount(id, account);
        else throw new AccountAlreadyExistsException("Ошибка. У клиента уже открыт такой счет.");
    }

    public void DeleteAccount(Guid id, AccountDb account)
    {
        var accounts = _clientStorage.Data.Accounts.Where(x => x.ClientId == id).ToList();
        if (accounts.Contains(account))
        {
            _clientStorage.DeleteAccount(id, account);
        } 
        else throw new NoSuchAccountException("Ошибка. У клиента нет такого счета.");
    }

    public void Update(ClientDb client)
    {
        var oldClient = Get(client.Id);
        if (oldClient != null)
        {
            oldClient.Name = client.Name;
            oldClient.Phone = client.Phone;
            oldClient.Birthday = client.Birthday;
            oldClient.Bonus = client.Bonus;
            _clientStorage.Update(oldClient);
        }        
    }

    public void Delete(ClientDb client)
    {
        var oldClient = Get(client.Id);
        if (oldClient != null)
        {
            _clientStorage.Delete(oldClient);
        }        
    }

    public List<ClientDb> GetClients(Filter filter)
    {
        var selection = _clientStorage.Data.Clients.
            Where(c => c.Birthday >= filter.DateFrom).
            Where(c => c.Birthday <= filter.DateBefore);

        if (filter.Name != null)
            selection = selection.Where(c => c.Name == filter.Name);

        if (filter.Phone != null)
            selection = selection.Where(c => c.Phone == filter.Phone);

        if (filter.Passport != 0)
            selection = selection.Where(c => c.Passport == filter.Passport);
        
        return selection.ToList();
    }
}