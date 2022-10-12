using ExportTool;
using Models;
using Services.Storages;
using Services;
using Xunit;
using Xunit.Abstractions;

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
    }
}
