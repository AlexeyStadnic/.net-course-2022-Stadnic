using Models;
using Services.Storages;
using Currency = Models.Currency;

namespace Services
{
    public class RateUpdater
    {
        public Task BillDollarsForEachClient(CancellationToken token)
        {
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);
            var clientsDb = clientStorage.Data.Clients.ToList();

            return Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    foreach (var clientDb in clientsDb)
                    {
                        var client = new Client();
                        client.Name = clientDb.Name;
                        client.Phone = clientDb.Phone;
                        client.Birthday = clientDb.Birthday;
                        client.Birthday = DateTime.SpecifyKind(clientDb.Birthday, DateTimeKind.Utc);
                        client.Bonus = clientDb.Bonus;
                        client.Passport = clientDb.Passport;

                        var accountDb = clientStorage.Data.Accounts.FirstOrDefault(x => x.ClientId == clientDb.Id && x.Currency.Code == 840);
                        var account = new Account();
                        var currency = new Currency();
                        var currencyDb = clientStorage.Data.Currencys.ToList();
                        currency.Code = currencyDb[0].Code;
                        currency.Name = currencyDb[0].Name;
                        account.Currency = currency;

                        if (accountDb.Amount == 0)
                        {
                            account.Amount = 100;
                        }
                        else
                        {
                            account.Amount = (int)((int)accountDb.Amount * 1.03);
                        }

                        clientService.UpdateAccount(client, account);
                    }
                    Task.Delay(5000);
                }                
            });                       
        }
    }
}
