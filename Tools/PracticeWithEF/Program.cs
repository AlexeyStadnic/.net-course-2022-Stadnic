﻿using Services;
using Services.Storages;

var clientStorage = new ClientStorage();
var clientService = new ClientService(clientStorage);

var filter = new Filter();
filter.Name = "Антонина";
filter.DateFrom = DateTime.Today.AddYears(-60);
filter.DateFrom = DateTime.SpecifyKind(filter.DateFrom, DateTimeKind.Utc);
filter.DateBefore = DateTime.Today;
filter.DateBefore = DateTime.SpecifyKind(filter.DateBefore, DateTimeKind.Utc);
var clients = clientService.GetClients(filter);

Console.WriteLine(clients.Count);