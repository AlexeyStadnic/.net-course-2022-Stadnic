using Models;
using ModelsDb;
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
            var client = new ClientDb();
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
            var client = new ClientDb();
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);

            // Act/Assert
            Assert.Throws<NoPassportException>(() => clientService.Add(client));
        }                
        
        [Fact]
        public void AccountAlreadyExistsExceptionTest()
        {
            // Arrange           
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);
            var clients = clientStorage.Data.Clients.ToList();
            Guid id = clients[0].Id;
            var client = clientService.Get(id);

            //Act
            var accountNew = new AccountDb();
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
        }

        [Fact]
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
            Assert.True(client.Equals(clients[0]));
            
        }

        [Fact]
        public void NoSuchAccountExceptionTest()
        {
            // Arrange
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);
            var clients = clientStorage.Data.Clients.ToList();
            Guid id = clients[0].Id;

            // Act
            var account = clientStorage.Data.Accounts.FirstOrDefault(x => x.ClientId == id);

            // Assert
            try
            {
                clientService.DeleteAccount(id, account);
            }
            catch (NoSuchAccountException e)
            {
                Assert.True(true);
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
            int i = 0;

            // Act
            foreach (Client client in clients)
            {
                var clientDB = new ClientDb();
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
            Assert.True(i == 5);            
        }

        [Fact]
        public void UpdatePositiveTest()
        {
            // Arrange      
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);
            var clients = clientStorage.Data.Clients.ToList();
            Guid id = clients[0].Id;
            var client = clientService.Get(id);
            client.Name = "Жорик";

            // Act
            clientService.Update(client);
            clientStorage.Data.SaveChanges();

            // Assert            
            Assert.True(clientService.Get(id).Name == client.Name);
        }

        [Fact]
        public void DeletePositiveTest()
        {
            // Arrange      
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);
            var clients = clientStorage.Data.Clients.ToList();
            Guid id = clients[0].Id;
            var client = clientService.Get(id);            

            // Act
            clientService.Delete(client);
            clientStorage.Data.SaveChanges();

            // Assert            
            Assert.True(clientService.Get(id) == null);
        }

        [Fact]
        public void SelectClientPositiveTest()
        {
            // Arrange      
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);            
            
            var filter = new Filter();
            filter.Name = "Антонина";
            filter.DateFrom = DateTime.Today.AddYears(-60);
            filter.DateFrom = DateTime.SpecifyKind(filter.DateFrom, DateTimeKind.Utc);
            filter.DateBefore = DateTime.Today;
            filter.DateBefore = DateTime.SpecifyKind(filter.DateBefore, DateTimeKind.Utc);

            // Act
            DateTime youngClientBirthday = clientService.GetClients(filter).Max(c => c.Birthday);
            var youngClient = clientService.GetClients(filter).
                FirstOrDefault(c => c.Birthday.Equals(youngClientBirthday));


            DateTime oldClientBirthday = clientService.GetClients(filter).Min(c => c.Birthday);
            var oldClient = clientService.GetClients(filter).
                FirstOrDefault(c => c.Birthday.Equals(oldClientBirthday));

            double averageAgeClient = clientService.GetClients(filter).
                Average(c => (DateTime.Now.Year - c.Birthday.Year));

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
