using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    [Table("accounts")]
    public class AccountDB
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("amount")]
        public int Amount { get; set; }

        [Column("currency_id")]
        public Guid CurrencyId { get; set; }
        public virtual CurrencyDB Currency { get; set; }

        [Column("client_id")]
        public Guid ClientId { get; set; }
        public virtual ClientDB Client { get; set; }
    }
}