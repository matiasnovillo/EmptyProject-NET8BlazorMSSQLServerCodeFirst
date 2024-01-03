using EmptyProject.Areas.Testing.Entities;

namespace EmptyProject.Areas.Testing.DTOs
{
    public class paginatedTestDTO
    {
        public List<Test?> lstTest { get; set; }
        public int TotalItems { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;
    }
}
