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
            var clientService = new ClientService();

            // Act/Assert
            Assert.Throws<YoungAgeException>(() => clientService.AddClient(client));
        }

        [Fact]
        public void NoPassportExceptionTest()
        {
            // Arrange
            var client = new Client();
            var clientService = new ClientService();

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
            
            var clientService = new ClientService();

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
            
            var clientService = new ClientService();

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
                clientService.EditAccount(client, accountNew);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Assert.True(false);
            }
        }
    }
}
