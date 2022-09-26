﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDB
{
    [Table("clients")]
    public class ClientDB
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
        public virtual ICollection<AccountDB> Accounts { get; set; }
    }
}
