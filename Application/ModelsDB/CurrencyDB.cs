namespace ModelsDB
{
    internal class CurrencyDB
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Code { get; set; }
        public Guid AccountID { get; set; }
    }
}
