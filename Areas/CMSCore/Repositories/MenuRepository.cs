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

        public IQueryable<MenuEntity> AsQueryable()
        {
            return _context.DbSetMenu
                        .AsQueryable();
        }

        #region Queries
        public async Task<int> Count(CancellationToken cancellationToken)
        {
            return await _context.DbSetMenu
                            .CountAsync();
        }

        public MenuEntity? GetById(int roleId, CancellationToken cancellationToken)
        {
            return _context.DbSetMenu
                        .FirstOrDefault(x => x.MenuId == roleId);
        }

        public List<MenuEntity?> GetAll(CancellationToken cancellationToken)
        {
            List<MenuEntity?> lstMenu = [];

            var GetAllQuery =
                    from x in _context.DbSetMenu
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
                MenuEntity roleEntity = new()
                {
                    MenuId = x.MenuId,
                    Name = x.Name
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

            int TotalMenu = await _context.DbSetMenu.CountAsync();

            var paginatedMenu = await _context.DbSetMenu
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
        public async Task<bool> Add(MenuEntity roleEntity, 
            CancellationToken cancellationToken)
        {
            await _context.DbSetMenu
                .AddAsync(roleEntity, cancellationToken);

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> Update(MenuEntity roleEntity, 
            CancellationToken cancellationToken)
        {
            _context.DbSetMenu
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
