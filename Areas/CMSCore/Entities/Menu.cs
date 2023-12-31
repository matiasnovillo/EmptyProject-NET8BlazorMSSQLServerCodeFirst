namespace EmptyProject.Areas.CMSCore.Entities
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string? Name { get; set; }
        public int MenuFatherId { get; set; }
        public int Order {  get; set; }
        public string? URLPath { get; set; }
        public string? IconURLPath { get; set; }
        public bool Active { get; set; }
    }
}
