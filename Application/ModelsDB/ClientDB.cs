using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDb
{
    [Table("clients")]
    public class ClientDb
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("passport")]
        public int Passport { get; set; }
        [Column("phone")]
        public string? Phone { get; set; }
        [Column("birthday")]
        public DateTime Birthday { get; set; }
        [Column("bonus")]
        public int Bonus { get; set; }
        public virtual ICollection<AccountDb> Accounts { get; set; }
    }
}
