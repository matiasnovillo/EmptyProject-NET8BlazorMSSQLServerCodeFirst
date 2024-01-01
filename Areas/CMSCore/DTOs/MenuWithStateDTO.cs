using EmptyProject.Areas.CMSCore.Entities;

namespace EmptyProject.Areas.CMSCore.DTOs
{
    public class MenuWithStateDTO : Menu
    {
        public bool IsSelected { get; set; }
    }
}
