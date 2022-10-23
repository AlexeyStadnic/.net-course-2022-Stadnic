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

    public void Add(Client client)
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

    public Client Get(Guid id)
    {        
        return _clientStorage.Get(id);
    }
    public void AddAccount(Client client, Account account)
    {       
        _clientStorage.AddAccount(client, account);
    }

    public void DeleteAccount(Client client, Account account)
    {        
        _clientStorage.DeleteAccount(client, account);
    }

    public void UpdateAccount(Client client, Account account)
    {
        _clientStorage.UpdateAccount(client, account);
    }

    public void Update(Client client)
    {        
        _clientStorage.Update(client);
    }

    public void Delete(Client client)
    {        
        _clientStorage.Delete(client);
    }

    public List<Client> GetClients(Filter filter)
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
        
        var clientsDb = selection.ToList();
        var clients = new List<Client>();

        foreach (var clientDb in clientsDb)
        {
            var client = new Client();
            client.Phone = clientDb.Phone;
            client.Birthday = clientDb.Birthday;
            client.Name = clientDb.Name;
            client.Passport = clientDb.Passport;
            client.Bonus = clientDb.Bonus;            

            clients.Add(client);
        }
        return clients;
    }
}