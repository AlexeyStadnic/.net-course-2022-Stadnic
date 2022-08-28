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
        
        stopWatch.Start();
        foreach (var client in clients)
        {
            if (searchPhone.Equals(client.Phone))
            {
                stopWatch.Stop();
                Console.WriteLine("Поиск в списке завершен. Имя клиента " + client.Name + 
                                  ", номер телефона " + client.Phone);
                Console.WriteLine("Время поиска " + stopWatch.ElapsedTicks);
            }
        }
      
        stopWatch.Restart();
        foreach (var client in clientsInDictionary)
        {
            if (searchPhone.Equals(client.Key))
            {
                stopWatch.Stop();
                Console.WriteLine("Поиск в словаре завершен. Имя клиента " + client.Value.Name + 
                                  ", номер телефона " + client.Key);
                Console.WriteLine("Время поиска " + stopWatch.ElapsedTicks);
            }
                
        }
        
        stopWatch.Restart();
        var searchClient = clientsInDictionary.FirstOrDefault(c => c.Key.Equals(searchPhone));
        stopWatch.Stop();
        Console.WriteLine("Поиск в словаре с использованием FirstOrDefault завершен. Имя клиента " + searchClient.Value.Name + 
                          ", номер телефона " + searchClient.Key);
        Console.WriteLine("Время поиска " + stopWatch.ElapsedTicks);
        
        SelectClientsBelowAge(clients,35);
        
        Employee employee = SelectEmployeeWithMinSalary(employees);
        Console.WriteLine("Сотрудник с минимальной зарплатой " + employee.Name);
        Console.WriteLine("Зарплата составляет " + employee.Salary);

        
        void SelectClientsBelowAge(List<Client> clients, int age)
        {
            int count = 0;
            Console.WriteLine("Клиенты младше " + age);
            foreach (Client client in clients)
            {
                if (DateTime.Today.Year - client.Birthday.Year < age)
                {
                    Console.WriteLine(client.Name + ", " + client.Birthday.Year + " года рождения");
                    count++;
                }
            }
            Console.WriteLine("Всего таких сотрудников " + count);
        }

        Employee SelectEmployeeWithMinSalary(List<Employee> employees)
        {
            int minSalary = Int32.MaxValue;
            Employee searchEmployee = new Employee();
            
            foreach (Employee employee in employees)
            {
                if (minSalary > employee.Salary)
                {
                    minSalary = employee.Salary;
                    searchEmployee = employee;
                }
            }
            
            return searchEmployee;
        }
    }
}
