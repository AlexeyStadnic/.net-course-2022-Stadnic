using ExportTool;
using Models;
using Services.Storages;
using Services;
using Xunit;

namespace ServiceTests
{
    public class CurrencyServicesTests
    {
        [Fact]
        public async void CurrencyConversionPositiveTest()
        {
            //Arrange     
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);
            var currencysDb = clientStorage.Data.Currencys.ToList();
            var currencyFromDb = currencysDb[0];
            var currencyFrom = new Currency();
            currencyFrom.Name = currencyFromDb.Name;
            var currencyToDb = currencysDb[1];
            var currencyTo = new Currency();
            currencyTo.Name = currencyToDb.Name;
            var currencyService = new CurrencyService();

            //Act            
            var currencyResponce = await currencyService.CurrencyConversion(currencyFrom,currencyTo,100);            
        }
    }
}
