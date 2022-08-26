using Models;
using Services;

class Program
{
    static void Main()
    {
        Console.WriteLine("Привет");
        TestDataGenerator testDataGenerator = new TestDataGenerator();
        List<Client> clients;
        clients = testDataGenerator.GenerateThousandClients();
        testDataGenerator.RandomAddClients(clients);
        
        foreach (Client client in clients)
        {
            Console.WriteLine("Имя: " + client.Name + ", Номер паспорта " + client.Passport + ", Телефон " + client.Phone + ", Год рождения " + client.Birthday.Year);
        }
    }
}
