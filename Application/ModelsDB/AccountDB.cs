using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDb
{
    [Table("accounts")]
    public class AccountDb
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("amount")]
        public int Amount { get; set; }

        [Column("currency_id")]
        public Guid CurrencyId { get; set; }
        public virtual CurrencyDb Currency { get; set; }

        [Column("client_id")]
        public Guid ClientId { get; set; }
        public virtual ClientDb Client { get; set; }
    }
}