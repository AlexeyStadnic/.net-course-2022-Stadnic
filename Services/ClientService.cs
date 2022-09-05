using Models;
using Services.Exceptions;

namespace Services;

public class ClientService
{
    public readonly List<Client> clients = new List<Client>();

    public void AddClient(Client client) 
    {
        var today = DateTime.Today;
        
        if (today.Year - client.Birthday.Year < 18)
        {
            throw new YongAgeException("Ошибка. Клиент слишком молод.");
        }
        
        if (client.Passport == 0)
        {
            throw new NoPassportException("Ошибка. У клиента отсутствует пасспорт.");
        }
        
        clients.Add(client);
    }
}