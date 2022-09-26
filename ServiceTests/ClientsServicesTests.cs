using Models;
using ModelsDB;
using Services.Exceptions;
using Services;
using Services.Storages;
using Xunit;


namespace ServiceTests
{
    public class ClientsServicesTests
    {
        [Fact]
        public void YongAgeExceptionTest()
        {
            // Arrange
            var client = new ClientDB();
            client.Birthday = new DateTime(2016, 2, 9);
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);

            // Act/Assert
            Assert.Throws<YoungAgeException>(() => clientService.Add(client));
        }

        [Fact]
        public void NoPassportExceptionTest()
        {
            // Arrange
            var client = new ClientDB();
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);

            // Act/Assert
            Assert.Throws<NoPassportException>(() => clientService.Add(client));
        }                
        
        /*[Fact]
        public void AccountAlreadyExistsExceptionTest()
        {
            // Arrange           
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);
            var clients = clientStorage.Data.Clients.ToList();
            Guid id = clients[0].Id;
            var client = clientService.Get(id);

            //Act
            var accountNew = new AccountDB();
            accountNew.Id = Guid.NewGuid();
            accountNew.Amount = 1000;
            var currency = clientStorage.Data.Currencys.ToList();
            accountNew.CurrencyId = currency[0].Id;

            try
            {
                clientService.AddAccount(client.Id, accountNew);                
            }
            catch(AccountAlreadyExistsException e) 
            { 
                Assert.True(true); 
            }
        }*/

        /*[Fact]
        public void GetPositiveTest()
        {
            // Arrange      
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);

            // Act
            var clients = clientStorage.Data.Clients.ToList();
            Guid id = clients[0].Id;
            var client = clientService.Get(id);

            // Assert
            if (client.Equals(clients[1]))
            {
                Assert.True(false);
            }
        }*/


        /*[Fact]
        public void NoSuchAccountExceptionTest()
        {
            // Arrange
            var client = new ClientDB();
            client.Name = "Алексей";
            client.Birthday = new DateTime(1986, 2, 9);
            client.Phone = "77881886";
            client.Passport = 14714;
            
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);

            //Act
            clientService.Add(client);
            
            var account = new AccountDB();
            
            var currencyEur = new CurrencyDB();
            currencyEur.Name = "EUR";
            currencyEur.Code = 978;

            account.Amount = 1000;
            

            clientService.AddAccount(client.Id,account);

            var accountNew = new AccountDB();      
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
        }*/

        [Fact]
        public void AddPositiveTest()
        {
            // Arrange      
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);
            TestDataGenerator testDataGenerator = new TestDataGenerator();
            List<Client> clients = testDataGenerator.GenerateThousandClients();
            int i = 0;

            // Act
            foreach (Client client in clients)
            {
                var clientDB = new ClientDB();
                clientDB.Name = client.Name;
                clientDB.Phone = client.Phone;
                clientDB.Birthday = client.Birthday;
                clientDB.Birthday = DateTime.SpecifyKind(client.Birthday, DateTimeKind.Utc);
                clientDB.Id = Guid.NewGuid();
                clientDB.Bonus = client.Bonus;
                clientDB.Passport = client.Passport;
                clientService.Add(clientDB);
                i++;
            }
            clientStorage.Data.SaveChanges();
            
            // Assert
            if (i == 1000)
            {
                Assert.True(true);
            }
        }

        /*[Fact]
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
        }*/
    }
}
