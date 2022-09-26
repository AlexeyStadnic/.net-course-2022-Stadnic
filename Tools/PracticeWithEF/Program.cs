using ModelsDb;
using Services;
using Services.Storages;

var clientStorage = new ClientStorage();
var clientService = new ClientService(clientStorage);

var currency = new CurrencyDb();
currency.Id = Guid.NewGuid();
currency.Name = "USD";
currency.Code = 840;
clientStorage.Data.Currencys.Add(currency); 
clientStorage.Data.SaveChanges();

var filter = new Filter();
filter.Name = "Антонина";
filter.DateFrom = DateTime.Today.AddYears(-60);
filter.DateFrom = DateTime.SpecifyKind(filter.DateFrom, DateTimeKind.Utc);
filter.DateBefore = DateTime.Today;
filter.DateBefore = DateTime.SpecifyKind(filter.DateBefore, DateTimeKind.Utc);
var clients = clientService.GetClients(filter);

Console.WriteLine(clients.Count);