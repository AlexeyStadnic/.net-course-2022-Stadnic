using Models;

namespace ModelsDB
{
    public class AccountDB
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public Currency Currency { get; set; }
        public Guid ClientID { get; set; }
    }
}