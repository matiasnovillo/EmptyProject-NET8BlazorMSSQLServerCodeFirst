using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using EmptyProject.Areas.CMSCore.DTOs;
using EmptyProject.Areas.CMSCore.Entities;
using EmptyProject.Areas.BasicCore.Entities.Configuration;

namespace EmptyProject.Areas.CMSCore.Repositories
{
    public class RoleMenuRepository
    {
        protected readonly EFCoreContext _context;

        public RoleMenuRepository(EFCoreContext context)
        {
            _context = context;
        }

        public IQueryable<RoleMenu> AsQueryable()
        {
            return _context.RoleMenu
                        .AsQueryable();
        }

        #region Queries
        public async Task<int> Count(CancellationToken cancellationToken)
        {
            return await _context.RoleMenu
                            .CountAsync();
        }

        public RoleMenu? GetById(int roleId, CancellationToken cancellationToken)
        {
            return _context.RoleMenu
                        .FirstOrDefault(x => x.RoleMenuId == roleId);
        }

        public List<RoleMenu?> GetAll(CancellationToken cancellationToken)
        {
            List<RoleMenu?> lstRoleMenu = [];

            var GetAllQuery =
                    from x in _context.RoleMenu
                    select new
                    {
                        x.RoleMenuId,
                        x.RoleId,
                        x.MenuId
                    };

            foreach (var x in GetAllQuery)
            {
                RoleMenu roleEntity = new()
                {
                    RoleMenuId = x.RoleMenuId,
                    RoleId = x.RoleId,
                    MenuId = x.MenuId
                };
                lstRoleMenu.Add(roleEntity);
            }

            return lstRoleMenu;
        }

        public async Task<paginatedRoleMenuDTO> GetAllByRoleMenuIdPaginated(string textToSearch,
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

            int TotalRoleMenu = await _context.RoleMenu.CountAsync();

            var paginatedRoleMenu = await _context.RoleMenu
                    .Where(x => strictSearch ?
                        words.All(word => x.RoleMenuId.ToString().Contains(word)) :
                        words.Any(word => x.RoleMenuId.ToString().Contains(word)))
                    .OrderBy(p => p.RoleMenuId)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

            return new paginatedRoleMenuDTO
            {
                lstRoleMenu = paginatedRoleMenu,
                TotalItems = TotalRoleMenu,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }

        public List<Menu> GetAllByRoleId(int roleId, List<Menu> lstMenu)
        {
            List<RoleMenu> lstRoleMenu = GetAll(CancellationToken.None);

            var lstMenuResult = (from rm in lstRoleMenu
                                      where rm.RoleId == roleId
                                      join m in lstMenu on rm.MenuId equals m.MenuId
                                      select m).ToList();

            return lstMenuResult;
        }

        public List<MenuWithStateDTO> GetAllWithStateByRoleId(int roleId, List<Menu> lstMenu)
        {
            List<RoleMenu> lstRoleMenu = GetAll(CancellationToken.None);

            var lstMenuWithState = lstMenu
                .Select(menu =>
                    new MenuWithStateDTO
                    {
                        MenuId = menu.MenuId,
                        Name = menu.Name,
                        MenuFatherId = menu.MenuFatherId,
                        Order = menu.Order,
                        URLPath = menu.URLPath,
                        IconURLPath = menu.IconURLPath,
                        Active = menu.Active,
                        IsSelected = lstRoleMenu
                            .Any(rm => rm.RoleId == roleId && rm.MenuId == menu.MenuId)
                    }
                ).ToList();

            return lstMenuWithState;
        }
        #endregion

        #region Non-Queries
        public async Task<bool> Add(RoleMenu roleEntity, 
            CancellationToken cancellationToken)
        {
            await _context.RoleMenu
                .AddAsync(roleEntity, cancellationToken);

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> Update(RoleMenu roleEntity, 
            CancellationToken cancellationToken)
        {
            _context.RoleMenu
                .Update(roleEntity);

            return await _context
                        .SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteByRoleMenuId(int roleId, 
            CancellationToken cancellationToken)
        {
            AsQueryable()
                .Where(x => x.RoleMenuId == roleId)
                .ExecuteDelete();

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
        #endregion
    }
}
