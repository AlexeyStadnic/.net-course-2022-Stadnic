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

            // Act/Assert
            Assert.Throws<DuplicateClientInDictionaryException>(() => clientService.AddClientInDictionary(client));
        }
    }
}
