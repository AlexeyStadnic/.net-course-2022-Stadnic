using Models;
using Services.Storages;

namespace Services
{
    public class CashDispenserService
    {
        public Task CashOut(Client client, Account account)
        {
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);

            return Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    if (account.Amount >= 100)
                    {
                        account.Amount -= 100;
                        clientService.UpdateAccount(client, account);
                    }                    
                    Task.Delay(1000);
                }
            });
        }
    }
}
