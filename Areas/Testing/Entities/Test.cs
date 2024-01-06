namespace EmptyProject.Areas.Testing.Entities
{
    public class Test
    {
        public int TestId { get; set; }
        public bool Boolean { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Decimal { get; set; }
        public int ForeignKeyDropdown { get; set; }
        public int Integer { get; set; }
        public string? Basic {  get; set; }
        public string? Email { get; set; }
        public string? File { get; set; }
        public string? HexColour { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Tag { get; set; }
        public string? TextArea { get; set; }
        public string? URL { get; set; }
        public TimeOnly Time {  get; set; }

        public string ToStringOnlyValuesForHTML()
        {
            return $@"";
        }
    }
}
