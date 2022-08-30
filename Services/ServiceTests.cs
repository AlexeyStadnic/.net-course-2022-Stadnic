using Models;
namespace Services;

public class EquivalenceTests
{
    public void GetHashCodeNecessityPositivTest()
    {
        TestDataGenerator testDataGenerator = new TestDataGenerator();    
        List<Client> clients = testDataGenerator.GenerateThousandClients();
        Dictionary<Client, Account> dictionary = testDataGenerator.GenerateDictionaryClients(clients);

        /*foreach (var client in dictionary)
        {
            Console.WriteLine(client.Key.Name + " , счет " + client.Value.Amount + client.Value.Currency.Name);
        }*/
        Console.WriteLine(dictionary[clients[0]]);
        Client newClient = new Client();
        newClient.Name = clients[0].Name;
        newClient.Passport = clients[0].Passport;   
        newClient.Phone = clients[0].Phone;
        newClient.Birthday = clients[0].Birthday;

        //Console.WriteLine(dictionary[newClient]);
    }
}