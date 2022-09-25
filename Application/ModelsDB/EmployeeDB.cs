namespace ModelsDB
{
    internal class EmployeeDB
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Passport { get; set; }
        public string? Phone { get; set; }
        public DateTime Birthday { get; set; }
        public int Bonus { get; set; }
        public string? Contract { get; set; }
        public int Salary { get; set; }
    }
}
