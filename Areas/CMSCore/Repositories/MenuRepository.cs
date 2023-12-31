using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using EmptyProject.Areas.CMSCore.DTOs;
using EmptyProject.Areas.CMSCore.Entities;
using EmptyProject.Areas.BasicCore.Entities.Configuration;

namespace EmptyProject.Areas.CMSCore.Repositories
{
    public class MenuRepository
    {
        protected readonly EFCoreContext _context;

        public MenuRepository(EFCoreContext context)
        {
            _context = context;
        }

        public IQueryable<Menu> AsQueryable()
        {
            return _context.Menu
                        .AsQueryable();
        }

        #region Queries
        public async Task<int> Count(CancellationToken cancellationToken)
        {
            return await _context.Menu
                            .CountAsync();
        }

        public Menu? GetById(int roleId, CancellationToken cancellationToken)
        {
            return _context.Menu
                        .FirstOrDefault(x => x.MenuId == roleId);
        }

        public List<Menu?> GetAll(CancellationToken cancellationToken)
        {
            List<Menu?> lstMenu = [];

            var GetAllQuery =
                    from x in _context.Menu
                    select new
                    {
                        x.MenuId,
                        x.Name,
                        x.MenuFatherId,
                        x.URLPath,
                        x.IconURLPath,
                        x.Active
                    };

            foreach (var x in GetAllQuery)
            {
                Menu roleEntity = new()
                {
                    MenuId = x.MenuId,
                    Name = x.Name,
                    MenuFatherId = x.MenuFatherId,
                    URLPath = x.URLPath,
                    IconURLPath = x.IconURLPath,
                    Active = x.Active
                };
                lstMenu.Add(roleEntity);
            }

            return lstMenu;
        }

        public async Task<paginatedMenuDTO> GetAllByMenuIdPaginated(string textToSearch,
            bool strictSearch,
            int pageIndex, 
            int pageSize,
            CancellationToken cancellationToken)
        {
            //textToSearch: "novillo matias  com" -> words: {novillo,matias,com}
            string[] words = Regex
                .Replace(textToSearch
                .Trim(), @"\s+", " ")
                .Split(" ");

            int TotalMenu = await _context.Menu.CountAsync();

            var paginatedMenu = await _context.Menu
                    .Where(x => strictSearch ?
                        words.All(word => x.MenuId.ToString().Contains(word)) :
                        words.Any(word => x.MenuId.ToString().Contains(word)))
                    .OrderBy(p => p.MenuId)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

            return new paginatedMenuDTO
            {
                lstMenu = paginatedMenu,
                TotalItems = TotalMenu,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }
        #endregion

        #region Non-Queries
        public async Task<bool> Add(Menu roleEntity, 
            CancellationToken cancellationToken)
        {
            await _context.Menu
                .AddAsync(roleEntity, cancellationToken);

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> Update(Menu roleEntity, 
            CancellationToken cancellationToken)
        {
            _context.Menu
                .Update(roleEntity);

            return await _context
                        .SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteByMenuId(int roleId, 
            CancellationToken cancellationToken)
        {
            AsQueryable()
                .Where(x => x.MenuId == roleId)
                .ExecuteDelete();

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
        #endregion
    }
}
