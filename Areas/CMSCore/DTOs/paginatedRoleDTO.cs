using EmptyProject.Areas.CMSCore.Entities;

namespace EmptyProject.Areas.CMSCore.DTOs
{
    public class paginatedRoleDTO
    {
        public List<Role?> lstRole { get; set; }
        public int TotalItems { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;
    }
}
