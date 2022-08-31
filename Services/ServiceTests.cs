using Models;
namespace Services;

public class EquivalenceTests
{
    public void GetHashCodeNecessityPositivTest()
    {
        TestDataGenerator testDataGenerator = new TestDataGenerator();    
        List<Client> clients = testDataGenerator.GenerateThousandClients();
        Dictionary<Client, Account> dictionary = testDataGenerator.GenerateDictionaryClients(clients);
        
        Client newClient = new Client();
        newClient.Name = clients[0].Name;
        newClient.Passport = clients[0].Passport;   
        newClient.Phone = clients[0].Phone;
        newClient.Birthday = clients[0].Birthday;    
        
        Account newAccount = dictionary[newClient];        
    }    
}