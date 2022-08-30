using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Models;
using Services;
using Bogus;

class Program
{
    static void Main()
    {
        TestDataGenerator testDataGenerator = new TestDataGenerator();
        List<Client> clients = testDataGenerator.GenerateThousandClients();
        List<Employee> employees = testDataGenerator.GenerateThousandEmployees();
        Dictionary<string, Client> clientsInDictionary = testDataGenerator.GenerateDictionaryFromClients(clients);
       
        string searchPhone = clients[999].Phone; 
        Stopwatch stopWatch = new Stopwatch();
        Client client = new Client();
        
        stopWatch.Start();
        client = clients.FirstOrDefault(c => c.Phone.Equals(searchPhone));
        stopWatch.Stop();
        Console.WriteLine("Поиск в списке завершен. Имя клиента " + client.Name + 
                          ", номер телефона " + client.Phone);
        Console.WriteLine("Время поиска " + stopWatch.ElapsedTicks);

        stopWatch.Restart();
        client = clientsInDictionary[searchPhone];
        stopWatch.Stop();
        Console.WriteLine("Поиск в словаре по ключу завершен. Имя клиента " + client.Name + 
                          ", номер телефона " + client.Phone);
        Console.WriteLine("Время поиска " + stopWatch.ElapsedTicks);
        
        stopWatch.Restart();
        var searchClient = clientsInDictionary.FirstOrDefault(c => c.Key.Equals(searchPhone));
        stopWatch.Stop();
        Console.WriteLine("Поиск в словаре с использованием FirstOrDefault завершен. Имя клиента " + searchClient.Value.Name + 
                          ", номер телефона " + searchClient.Key);
        Console.WriteLine("Время поиска " + stopWatch.ElapsedTicks);
        
        // выбираем клиентов младше 35 лет
        clients.Where(c => (DateTime.Today.Year - c.Birthday.Year) < 35).ToList();
        
        // находим сотрудника с минимальной зарплатой
        int minSalary = employees.Min(c => c.Salary);
        Employee employeeWithMinSalary = employees.First(c => c.Salary == minSalary);
        Console.WriteLine("Сотрудник с минимальной зарплатой " + employeeWithMinSalary.Name);
        Console.WriteLine("Зарплата составляет " + employeeWithMinSalary.Salary);

        EquivalenceTests equivalenceTests = new EquivalenceTests();
        equivalenceTests.GetHashCodeNecessityPositivTest();
    }
}
