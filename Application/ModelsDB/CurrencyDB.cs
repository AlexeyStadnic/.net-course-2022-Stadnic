using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    [Table("currencys")]
    public class CurrencyDB
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("code")]
        public int Code { get; set; }
        public virtual ICollection<AccountDB> Accounts { get; set; }
    }
}
