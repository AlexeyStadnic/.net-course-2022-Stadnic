using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using ModelsDb;
using Services;
using Services.Storages;

namespace Bank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {        
        ClientService clientService;
        public ClientController()
        {
            ClientStorage clientStorage = new ClientStorage();
            clientService = new ClientService(clientStorage);
        }        

        [HttpGet]
        public Client GetClient(Guid id)
        {
            return clientService.Get(id);
        }

        [HttpPost]
        public void AddClient(Client client)
        {
            clientService.Add(client);
        }

        [HttpPut]
        public void UpdateClient(Client client)
        {
            clientService.Update(client);
        }

        [HttpDelete]
        public void DeleteClient(Client client)
        {
            clientService.Delete(client);
        }
    }
}
