using ModelsDB;
using Services;
using Services.Storages;

var clientStorage = new ClientStorage();
var clientService = new ClientService(clientStorage);

var currency = new CurrencyDB();
currency.Name = "USD";
currency.Code = 840;
currency.Id = Guid.NewGuid();

clientStorage.Data.Currencys.Add(currency);
clientStorage.Data.SaveChanges();

/*var clients = clientStorage.Data.Clients.ToList();
Guid id = clients[0].Id;
var client = clientService.Get(id);

var account = new AccountDB();
account.Id = Guid.NewGuid();
var currency = clientStorage.Data.Currencys.ToList();
account.CurrencyId = currency[1].Id;
account.Amount = 900;

clientService.AddAccount(id, account);*/