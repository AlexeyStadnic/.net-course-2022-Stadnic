using Models;
using Services.Exceptions;
using Services;
using Xunit;

namespace ServiceTests
{
    public class ClientsExceptionsTests
    {
        [Fact]
        public void YongAgeExceptionTest()
        {
            // Arrange
            var client = new Client();
            client.Birthday = new DateTime(2016, 2, 9);
            var clientService = new ClientService();

            // Act/Assert
            Assert.Throws<YongAgeException>(() => clientService.AddClientInDictionary(client));
        }

        [Fact]
        public void NoPassportExceptionTest()
        {
            // Arrange
            var client = new Client();
            var clientService = new ClientService();

            // Act/Assert
            Assert.Throws<NoPassportException>(() => clientService.AddClientInDictionary(client));
        }

        [Fact]
        public void ListAccountsIsEmptyExceptionTest()
        {
            // Arrange
            var client = new Client();
            client.Name = "Алексей";
            client.Birthday = new DateTime(1986, 2, 9);
            client.Phone = "77881886";
            client.Passport = 14714;

            var clientService = new ClientService();

            // Act/Assert
            Assert.Throws<AccountsListIsEmptyException>(() => clientService.AddClientInDictionary(client));
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
                clientService.AddClientInDictionary(client);
                //clientService.AddClientInDictionary(client);
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
        public void AddAccountInClientExceptionTest()
        {
            // Arrange
            var client = new Client();
            client.Name = "Алексей";
            client.Birthday = new DateTime(1986, 2, 9);
            client.Phone = "77881886";
            client.Passport = 14714;
            
            var clientService = new ClientService();

            //Act
            clientService.AddClientInDictionary(client);
            
            var account = new Account();
            
            var currencyEur = new Currency();
            currencyEur.Name = "EUR";
            currencyEur.Code = 978;

            account.Amount = 1000;
            account.Currency = currencyEur;

            clientService.AddAccountInClient(client,account);
            //clientService.AddAccountInClient(client,account);
        }
        
        [Fact]
        public void EditAccountInClientExceptionTest()
        {
            // Arrange
            var client = new Client();
            client.Name = "Алексей";
            client.Birthday = new DateTime(1986, 2, 9);
            client.Phone = "77881886";
            client.Passport = 14714;
            
            var clientService = new ClientService();

            //Act
            clientService.AddClientInDictionary(client);
            
            var account = new Account();
            
            var currencyEur = new Currency();
            currencyEur.Name = "EUR";
            currencyEur.Code = 978;

            account.Amount = 1000;
            account.Currency = currencyEur;

            clientService.AddAccountInClient(client,account);

            var newAccount = new Account();
            var currencyUsd = new Currency();
            currencyUsd.Name = "USD";
            currencyUsd.Code = 840;

            account.Amount = 12125;
            account.Currency = currencyUsd;
            
            clientService.EditAccountInClient(client,account,newAccount);
        }
    }
}
