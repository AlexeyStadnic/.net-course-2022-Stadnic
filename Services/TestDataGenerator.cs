using Bogus;
using Models;

namespace Services;

public class TestDataGenerator
{
    public List<Client> GenerateThousandClients() 
    { 
        List<Client> clients = new List<Client>();
        for (int i = 0; i < 1000; i++)
            clients.Add(new Client());
        return clients; 
    }

    public List<Employee> GenerateThousandEmployees()
    {
        List<Employee> employees = new List<Employee>();
        for (int i = 0; i < 1000; i++)
            employees.Add(new Employee());
        return employees;
    }

    public void RandomAddClients(List<Client> clients)
    {
        char[] letters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        Random random = new Random();
        int numberOfLettersInName;
        string name;
        
        DateTime today = DateTime.Today;
        DateTime dateEnd = new DateTime(today.Year - 60, today.Month, today.Day); // граница пенсионного возраста
        DateTime dateStart = new DateTime(today.Year - 18, today.Month, today.Day); // граница совершенолетия
        int allowableAgeEnd = (today - dateEnd).Days;
        int allowableAgeStart = (today - dateStart).Days;

        foreach (Client client in clients)
        {
            name = "";
            numberOfLettersInName = random.Next(1,21); 
            for (int i = 0; i < numberOfLettersInName; i++)
            {
                int letter_num = random.Next(0, letters.Length - 1);
                name += letters[letter_num];
            }
            name = char.ToUpper(name[0]) + name.Substring(1);
            client.Name = name;
            client.Passport = random.Next(1,10000000);
            client.Phone = random.Next(77700000,78000000).ToString();
            client.Birthday = dateEnd.AddDays(random.Next(allowableAgeEnd - allowableAgeStart));            
        }
    }

    public void RandomAddClientsBogus(List<Client> clients)
    {
        DateTime today = DateTime.Today;
        DateTime dateEnd = new DateTime(today.Year - 60, today.Month, today.Day); // граница пенсионного возраста
        DateTime dateStart = new DateTime(today.Year - 18, today.Month, today.Day); // граница совершенолетия

        var testClient = new Faker<Client>("ru")
            .RuleFor(c => c.Name, f => f.Name.FirstName())
            .RuleFor(c => c.Passport, f => f.Random.Int(1, 9999999))
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumberFormat())
            .RuleFor(c => c.Birthday, f => f.Date.Between(dateStart,dateEnd));   

        foreach (Client client in clients)
        {
            client.Name = testClient.Generate().Name;
            client.Passport = testClient.Generate().Passport;
            client.Phone = testClient.Generate().Phone;
            client.Birthday = testClient.Generate().Birthday;
        }        
    }
}