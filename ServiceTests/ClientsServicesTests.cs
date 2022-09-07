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
            Assert.Throws<YongAgeException>(() => clientService.AddClient(client));
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
        public void DuplicateClientInDictionaryExceptionTest()
        {
            // Arrange
            var client = new Client();
            client.Name = "Алексей";
            client.Birthday = new DateTime(1986, 2, 9);
            client.Phone = "77881886";
            client.Passport = 14714;

            var clientService = new ClientService();

            //Act
            try
            {
                clientService.AddClient(client);
                //clientService.AddClient(client);
            }
            catch (ArgumentException e)
            {
                Assert.True(false);
            }    
            catch (Exception e)
            {
                Assert.True(false);
            }            
        }
        
        [Fact]
        public void AddAccountExceptionTest()
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

            var account2 = new Account();
            var currencyEur2 = new Currency();
            currencyEur2.Name = "EUR";
            currencyEur2.Code = 978;
            account2.Amount = 1000;
            account2.Currency = currencyEur2;

            try
            {
                clientService.AddAccount(client, account);
                //clientService.AddAccount(client,account2);
            }
            catch(KeyNotFoundException e) 
            { 
                Assert.True(false); 
            }
        }
        
        [Fact]
        public void EditAccountExceptionTest()
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

            account.Amount = 12125;           

            try
            {
                clientService.EditAccount(client, account);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Assert.True(false);
            }
        }
    }
}
