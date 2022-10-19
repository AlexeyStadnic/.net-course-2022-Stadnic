using Models;
using Services.Storages;
using Services;
using Xunit;

namespace ExportTool
{
    public class ExportServiceTests
    {
        [Fact]
        public void ExportClientPositiveTest()
        {
            //Arrange
            string pathToDirectory = Path.Combine("C:", "Users", "user", "source", "repos",
                ".net-course-2022-Stadnic", "Tools", "TestFiles");
            string fileName = "clients.csv";
            ExportService exportService = new ExportService(pathToDirectory, fileName);

            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);
            var clientsDB = clientStorage.Data.Clients.ToList();
            List<Client> clients = new List<Client>();
            for (int i = 0; i < clientsDB.Count; i++)
            {
                Guid id = clientsDB[i].Id;
                var client = clientService.Get(id);
                clients.Add(client);
            }

            //Act            
            exportService.WriteClientToCsv(clients);            
            var clientsFromFile = exportService.ReadClientFromCsv();

            //Assert
            Assert.Equal(clients[0].Name, clientsFromFile[0].Name);
        }

        [Fact]
        public void ExportClientJSONPositiveTest()
        {
            //Arrange
            string pathToDirectory = Path.Combine("C:", "Users", "user", "source", "repos",
                ".net-course-2022-Stadnic", "Tools", "TestFiles");
            string fileName = "clients.json";
            ExportService exportService = new ExportService(pathToDirectory, fileName);

            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);
            var clientsDB = clientStorage.Data.Clients.ToList();
            List<Client> clients = new List<Client>();
            for (int i = 0; i < clientsDB.Count; i++)
            {
                Guid id = clientsDB[i].Id;
                var client = clientService.Get(id);
                clients.Add(client);
            }

            //Act            
            string path = Path.Combine(pathToDirectory,fileName);
            exportService.WriteToJSON(clients, path);            
        }

        [Fact]
        public void AddClientFromCSVToDBPositiveTest()
        {
            //Arrange
            string pathToDirectory = Path.Combine("C:", "Users", "user", "source", "repos",
                ".net-course-2022-Stadnic", "Tools", "TestFiles");
            string fileName = "clientsExport.csv";
            ExportService exportService = new ExportService(pathToDirectory, fileName);

            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);
            var clientsFromFile = exportService.ReadClientFromCsv();
            int i = 0;

            //Act
            foreach (var client in clientsFromFile)
            {
                clientService.Add(client);
                i++;
            }
            clientStorage.Data.SaveChanges();

            //Assert
            Assert.True(clientsFromFile.Count == i);
        }

        [Fact]
        public void AddClientFromJSONToDBPositiveTest()
        {
            //Arrange
            string pathToDirectory = Path.Combine("C:", "Users", "user", "source", "repos",
                ".net-course-2022-Stadnic", "Tools", "TestFiles");
            string fileName = "clientsExport.json";
            string path = Path.Combine(pathToDirectory,fileName);
            ExportService exportService = new ExportService(pathToDirectory, fileName);

            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);
            var clientsFromFile = exportService.ReadFromJSON<Client>(path);
            int i = 0;

            //Act
            foreach (var client in clientsFromFile)
            {
                clientService.Add(client);
                i++;
            }
            clientStorage.Data.SaveChanges();

            //Assert
            Assert.True(clientsFromFile.Count == i);
        }
    }
}
