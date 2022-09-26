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
            var client = new Client();
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
            var client = new Client();
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
            var accountNew = new Account();
            var currencyNew = new Currency();
           
            accountNew.Amount = 1000;
            var currency = clientStorage.Data.Currencys.ToList();
            currencyNew.Code = currency[0].Code;
            currencyNew.Name = currency[0].Name;
            accountNew.Currency = currencyNew;           

            try
            {
                clientService.AddAccount(client, accountNew);                
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
            var client = new Client();
            client.Phone = clients[0].Phone;
            client.Birthday = clients[0].Birthday;
            client.Name = clients[0].Name;
            client.Passport = clients[0].Passport;
            client.Bonus = clients[0].Bonus;

            // Act
            var accountDb = clientStorage.Data.Accounts.FirstOrDefault(x => x.ClientId == clients[0].Id);
            var account = new Account();
            account.Amount = accountDb.Amount;
            var currency = new Currency();
            currency.Name = accountDb.Currency.Name;
            currency.Code = accountDb.Currency.Code;
            account.Currency = currency;

            // Assert
            try
            {
                clientService.DeleteAccount(client, account);
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
                clientService.Add(client);
                i++;
            }
            clientStorage.Data.SaveChanges();
            
            // Assert            
            Assert.True(i == 1000);            
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
            Assert.False(clientStorage.Data.Clients.Contains(clients[0]));
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
