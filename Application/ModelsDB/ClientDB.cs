namespace ModelsDB
{
    internal class ClientDB
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Passport { get; set; }
        public string? Phone { get; set; }
        public DateTime Birthday { get; set; }
        public int Bonus { get; set; }
    }
}
