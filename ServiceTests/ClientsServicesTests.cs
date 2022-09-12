using Models;
using Services.Exceptions;
using Services;
using Xunit;


namespace ServiceTests
{
    public class ClientsServicesTests
    {
        [Fact]
        public void YongAgeExceptionTest()
        {
            // Arrange
            var client = new Client();
            client.Birthday = new DateTime(2016, 2, 9);
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);

            // Act/Assert
            Assert.Throws<YoungAgeException>(() => clientService.AddClient(client));
        }

        [Fact]
        public void NoPassportExceptionTest()
        {
            // Arrange
            var client = new Client();
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);

            // Act/Assert
            Assert.Throws<NoPassportException>(() => clientService.AddClient(client));
        }                
        
        [Fact]
        public void AccountAlreadyExistsExceptionTest()
        {
            // Arrange
            var client = new Client();
            client.Name = "Алексей";
            client.Birthday = new DateTime(1986, 2, 9);
            client.Phone = "77881886";
            client.Passport = 14714;
            
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);

            //Act
            clientService.AddClient(client);
            
            var account = new Account();
            
            var currencyEur = new Currency();
            currencyEur.Name = "EUR";
            currencyEur.Code = 978;

            account.Amount = 1000;
            account.Currency = currencyEur;            

            try
            {
                clientService.AddAccount(client, account);                
            }
            catch(KeyNotFoundException e) 
            { 
                Assert.True(false); 
            }
        }
        
        [Fact]
        public void NoSuchAccountExceptionTest()
        {
            // Arrange
            var client = new Client();
            client.Name = "Алексей";
            client.Birthday = new DateTime(1986, 2, 9);
            client.Phone = "77881886";
            client.Passport = 14714;
            
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);

            //Act
            clientService.AddClient(client);
            
            var account = new Account();
            
            var currencyEur = new Currency();
            currencyEur.Name = "EUR";
            currencyEur.Code = 978;

            account.Amount = 1000;
            account.Currency = currencyEur;

            clientService.AddAccount(client,account);

            var accountNew = new Account();      
            accountNew.Amount = 12125;
            accountNew.Currency = currencyEur;

            try
            {
                clientService.UpdateAccount(client, accountNew);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Assert.True(false);
            }
            catch (NullReferenceException e)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void AddPositiveTest()
        {
            // Arrange      

            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);
            TestDataGenerator testDataGenerator = new TestDataGenerator();
            List<Client> clients = testDataGenerator.GenerateThousandClients();

            // Act
            foreach (Client client in clients)
            {
                clientService.AddClient(client);
            }
            
            // Assert
            if (clientStorage._dictionaryClients.Count != 1000)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void SelectClientPositiveTest()
        {
            // Arrange      

            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);
            TestDataGenerator testDataGenerator = new TestDataGenerator();
            List<Client> clients = testDataGenerator.GenerateThousandClients();
            var filter = new Filter();
            filter.Name = "Алексей";
            filter.DateFrom = DateTime.Today.AddYears(-60);
            filter.DateBefore = DateTime.Today;
            
            foreach (Client client in clients)
            {
                clientService.AddClient(client);
            }

            // Act
            DateTime youngClientBirthday = clientService.GetClients(filter).Max(c => c.Key.Birthday);
            var youngClient = clientService.GetClients(filter).
                FirstOrDefault(c => c.Key.Birthday.Equals(youngClientBirthday));
            

            DateTime oldClientBirthday = clientService.GetClients(filter).Min(c => c.Key.Birthday);
            var oldClient = clientService.GetClients(filter).
                FirstOrDefault(c => c.Key.Birthday.Equals(oldClientBirthday));

            double averageAgeClient = clientService.GetClients(filter).
                Average(c => (DateTime.Now.Year - c.Key.Birthday.Year));

            // Assert
            if (averageAgeClient > 18)
            {
                Assert.True(true);
            }
            else
            {
                Assert.True(false);
            }            
        }
    }
}
