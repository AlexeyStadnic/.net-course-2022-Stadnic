using ExportTool;
using Models;
using Services.Storages;
using Services;
using Xunit;
using Xunit.Abstractions;
using ModelsDb;

namespace ServiceTests
{
    public class ThreadAndTaskTests
    {
        private ITestOutputHelper _output;

        public Account account = new Account();
        public Currency currency = new Currency();

        public Object locker = new Object();
        
        public ThreadAndTaskTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void AccrualMoneyPositiveTest()
        {
            currency.Name = "USD";
            currency.Code = 840;
            account.Currency = currency;
            account.Amount = 0;

            var flowOne = new Thread(AccrualMoney);
            flowOne.Name = "flowOne";
            var flowTwo = new Thread(AccrualMoney);
            flowTwo.Name = "flowTwo";
            
            flowOne.Start();            
            flowTwo.Start();

            Thread.Sleep(50000);
            Assert.True(true);            
        }

        void AccrualMoney()
        {
            
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(100);
                lock (locker)
                {
                    account.Amount += 100;
                }
                _output.WriteLine($"{Thread.CurrentThread.Name}: {account.Amount}");
            }
        }

        [Fact]
        public void ParallelImportExportClientPositiveTest()
        {
            //Arrange
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);

            string pathToDirectoryImport = Path.Combine("C:", "Users", "user", "source", "repos",
                ".net-course-2022-Stadnic", "Tools", "TestFiles");
            string fileNameImport = "clients.csv";
            ExportService exportServiceImport = new ExportService(pathToDirectoryImport, fileNameImport);            
            var clientsFromFile = exportServiceImport.ReadClientFromCsv();            

            string pathToDirectoryExport = Path.Combine("C:", "Users", "user", "source", "repos",
                ".net-course-2022-Stadnic", "Tools", "TestFiles");
            string fileNameExport = "clientsExport.csv";
            ExportService exportServiceExport = new ExportService(pathToDirectoryExport, fileNameExport);
            var clientsDB = clientStorage.Data.Clients.ToList();
            List<Client> clients = new List<Client>();

            var flowImport = new Thread(() =>
            {
                foreach (var client in clientsFromFile)
                {
                    clientService.Add(client);                    
                }
                clientStorage.Data.SaveChanges();
                _output.WriteLine($"{Thread.CurrentThread.Name}: клиенты добавлены в базу");
            });
            flowImport.Name = "Import";

            var flowExport = new Thread(() =>
            {
                for (int j = 0; j < clientsDB.Count; j++)
                {
                    Guid id = clientsDB[j].Id;
                    var client = clientService.Get(id);
                    clients.Add(client);
                }
                exportServiceExport.WriteClientToCsv(clients);
                _output.WriteLine($"{Thread.CurrentThread.Name}: клиенты добавлены в csv");
            });
            flowExport.Name = "Export";

            //Act
            flowImport.Start();
            Thread.Sleep(100000);
            flowExport.Start();
            Thread.Sleep(50000);
        }

        [Fact]
        public void RateUpdaterPositiveTest()
        {
            //Arrange
            var cancellation = new CancellationTokenSource(); 
            var token = cancellation.Token;
            var rateUpdater = new RateUpdater();

            //Act
            Task task = rateUpdater.BillDollarsForEachClient(token);
            task.Wait(25000);
            cancellation.Cancel();
        }

        [Fact]
        public void CashDispenserPositiveTest()
        {
            //Arrange            
            var cash = new CashDispenserService();
            var tasks = new List<Task>();

            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);
            var clientsDb = clientStorage.Data.Clients.ToList();

            //Act
            for (int i = 0; i < 10; i++)
            {
                var client = new Client();
                client.Name = clientsDb[i].Name;
                client.Phone = clientsDb[i].Phone;
                client.Birthday = clientsDb[i].Birthday;
                client.Birthday = DateTime.SpecifyKind(clientsDb[i].Birthday, DateTimeKind.Utc);
                client.Bonus = clientsDb[i].Bonus;
                client.Passport = clientsDb[i].Passport;

                var accountDb = clientStorage.Data.Accounts.
                    FirstOrDefault(x => x.ClientId == clientsDb[i].Id && x.Currency.Code == 840);                
                var currencyDb = clientStorage.Data.Currencys.ToList();
                currency.Code = currencyDb[0].Code;
                currency.Name = currencyDb[0].Name;
                account.Currency = currency;
                account.Amount = accountDb.Amount;

                tasks.Add(cash.CashOut(client, account));
                Task.Delay(500);
            }

            foreach (var task in tasks)
            {
                task.Wait();
            }
        }
    }
}
