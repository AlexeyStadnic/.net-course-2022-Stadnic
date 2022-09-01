using Bogus;
using Models;

namespace Services;

public class TestDataGenerator
{
    public List<Client> GenerateThousandClients() 
    { 
        List<Client> clients = new List<Client>();
        DateTime today = DateTime.Today;
        DateTime dateEnd = new DateTime(today.Year - 60, today.Month, today.Day); // граница пенсионного возраста
        DateTime dateStart = new DateTime(today.Year - 18, today.Month, today.Day); // граница совершенолетия

        var testClient = new Faker<Client>("ru")
            .RuleFor(c => c.Name, f => f.Name.FirstName())
            .RuleFor(c => c.Passport, f => f.Random.Int(1, 9999999))
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumberFormat())
            .RuleFor(c => c.Birthday, f => f.Date.Between(dateStart,dateEnd));  
        
        for (int i = 0; i < 1000; i++)
        {
            Client client = new Client();
            client.Name = testClient.Generate().Name;
            client.Passport = testClient.Generate().Passport;
            client.Phone = testClient.Generate().Phone;
            client.Birthday = testClient.Generate().Birthday;
            clients.Add(client);
        }
        return clients; 
    }

    public List<Employee> GenerateThousandEmployees()
    {
        List<Employee> employees = new List<Employee>();
        DateTime today = DateTime.Today;
        DateTime dateEnd = new DateTime(today.Year - 60, today.Month, today.Day); // граница пенсионного возраста
        DateTime dateStart = new DateTime(today.Year - 18, today.Month, today.Day); // граница совершенолетия

        var testEmployee = new Faker<Employee>("ru")
            .RuleFor(e => e.Name, f => f.Name.FirstName())
            .RuleFor(e => e.Passport, f => f.Random.Int(1, 9999999))
            .RuleFor(e => e.Phone, f => f.Phone.PhoneNumberFormat())
            .RuleFor(e => e.Birthday, f => f.Date.Between(dateStart,dateEnd))
            .RuleFor(e => e.Salary, f => f.Random.Int(0,10000));
        
        for (int i = 0; i < 1000; i++)
        {
            Employee employee = new Employee();
            employee.Name = testEmployee.Generate().Name;
            employee.Passport = testEmployee.Generate().Passport;
            employee.Phone = testEmployee.Generate().Phone;
            employee.Birthday = testEmployee.Generate().Birthday;
            employee.Salary = testEmployee.Generate().Salary;
            employees.Add(employee);
        }
        return employees;
    }

    public Dictionary<string, Client> GenerateDictionaryFromClients(List<Client> clients)
    {
        Dictionary<string, Client> dictionary = new Dictionary<string, Client>();
        foreach (var client in clients)
        {
            dictionary.Add(client.Phone,client);
        }
        return dictionary;
    }

    public Dictionary<Client, List<Account>> GenerateDictionaryClients(List<Client> clients)
    {      
        Dictionary<Client, List<Account>> dictionary = new Dictionary<Client, List<Account>>();
        Currency currencyUSD = new Currency();
        currencyUSD.Name = "USD";
        currencyUSD.Code = 840;
        Currency currencyEUR = new Currency();
        currencyEUR.Name = "EUR";
        currencyEUR.Code = 978;

        var testAccount = new Faker<Account>("ru")
            .RuleFor(a => a.Amount, f => f.Random.Int(1, 10000000));                  

        foreach (var client in clients)
        {
            List<Account> accounts = new List<Account>();
            
            Account accountUSD = new Account();
;           accountUSD.Amount = testAccount.Generate().Amount;
            accountUSD.Currency = currencyUSD;
            accounts.Add(accountUSD);
            
            Account accountEUR = new Account();
            accountEUR.Amount = testAccount.Generate().Amount;
            accountEUR.Currency = currencyEUR;
            accounts.Add(accountEUR);
            
            dictionary.Add(client,accounts);
        }
        return dictionary;
    }
}