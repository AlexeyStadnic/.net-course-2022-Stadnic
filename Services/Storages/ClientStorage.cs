using ModelsDb;
using Models;
using Services.Exceptions;

namespace Services.Storages;
public class ClientStorage : IClientStorage
{
    public BankContext Data { get; }

    public ClientStorage()
    {
        Data = new BankContext();
    }

    public void Add(Client client)
    {
        var clientDb = new ClientDb();
        clientDb.Name = client.Name;
        clientDb.Phone = client.Phone;
        clientDb.Birthday = client.Birthday;
        clientDb.Birthday = DateTime.SpecifyKind(client.Birthday, DateTimeKind.Utc);
        clientDb.Id = Guid.NewGuid();
        clientDb.Bonus = client.Bonus;
        clientDb.Passport = client.Passport;

        var defaultAccount = new AccountDb();
        defaultAccount.ClientId = clientDb.Id;
        defaultAccount.Amount = 0;
        var currencys = Data.Currencys.ToList();
        defaultAccount.CurrencyId = currencys[0].Id;        

        Data.Clients.Add(clientDb);
        Data.Accounts.Add(defaultAccount);
        Data.SaveChanges();
    }

    public Client Get(Guid id)
    {
        var clientDb = Data.Clients.FirstOrDefault(c => c.Id == id);
        var client = new Client();
        if (clientDb != null)
        {
            client.Phone = clientDb.Phone;
            client.Birthday = clientDb.Birthday;
            client.Name = clientDb.Name;
            client.Passport = clientDb.Passport;
            client.Bonus = clientDb.Bonus;
        }        
        return client;
    }

    public void Delete(Client client)
    {
        var oldClient = Data.Clients.FirstOrDefault(c => c.Phone.Equals(client.Phone));
        if (oldClient != null)
        {
            Data.Clients.Remove(oldClient);
            Data.SaveChanges();
        }                 
    }

    public void Update(Client client)
    {
        var oldClient = Data.Clients.FirstOrDefault(c => c.Passport == client.Passport);
        if (oldClient != null)
        {
            oldClient.Name = client.Name;
            oldClient.Phone = client.Phone;
            oldClient.Birthday = client.Birthday;
            oldClient.Bonus = client.Bonus;
            Data.Clients.Update(oldClient);
        }        
        Data.SaveChanges();
    }

    public void AddAccount(Client client, Account account)
    {
        var clientDb = Data.Clients.FirstOrDefault(c => c.Phone.Equals(client.Phone));
        if (clientDb != null)
        {
            var accountsDb = Data.Accounts.Where(x => x.Currency.Code == account.Currency.Code).ToList();
            if (accountsDb.Count == 0)
            {
                var accountDb = new AccountDb();
                accountDb.ClientId = clientDb.Id;
                accountDb.Id = Guid.NewGuid();
                accountDb.Amount = account.Amount;
                var currencyDb = Data.Currencys.FirstOrDefault(c => c.Code == account.Currency.Code);
                accountDb.CurrencyId = currencyDb.Id;

                Data.Accounts.Add(accountDb);
                Data.SaveChanges();
            }
            else throw new AccountAlreadyExistsException("Ошибка. У клиента уже открыт такой счет.");
        }       
    }
    
    public void UpdateAccount(Client client, Account account)
    {
        
    }

    public void DeleteAccount(Client client, Account account)
    {        
        var clientDb = Data.Clients.FirstOrDefault(c => c.Phone.Equals(client.Phone));
        if (clientDb != null)
        {            
            var accountDb = Data.Accounts.Where(a => a.ClientId == clientDb.Id)
                .Where(a => a.Currency.Code == account.Currency.Code).FirstOrDefault();

            if (accountDb != null)
            {
                Data.Accounts.Remove(accountDb);
                Data.SaveChanges();
            }
            else throw new NoSuchAccountException("Ошибка. У клиента нет такого счета.");
        }        
    }
}