using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    [Table("employees")]
    public class EmployeeDB
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
        [Column("contract")]
        public string? Contract { get; set; }
        [Column("salary")]
        public int Salary { get; set; }
    }
}
