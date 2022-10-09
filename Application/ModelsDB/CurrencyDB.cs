using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDb
{
    [Table("currencys")]
    public class CurrencyDb
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("code")]
        public int Code { get; set; }
        public virtual ICollection<AccountDb> Accounts { get; set; }
    }
}
