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

        public IQueryable<RoleMenuEntity> AsQueryable()
        {
            return _context.DbSetRoleMenu
                        .AsQueryable();
        }

        #region Queries
        public async Task<int> Count(CancellationToken cancellationToken)
        {
            return await _context.DbSetRoleMenu
                            .CountAsync();
        }

        public RoleMenuEntity? GetById(int roleId, CancellationToken cancellationToken)
        {
            return _context.DbSetRoleMenu
                        .FirstOrDefault(x => x.RoleMenuId == roleId);
        }

        public List<RoleMenuEntity?> GetAll(CancellationToken cancellationToken)
        {
            List<RoleMenuEntity?> lstRoleMenu = [];

            var GetAllQuery =
                    from x in _context.DbSetRoleMenu
                    select new
                    {
                        x.RoleMenuId,
                        x.RoleId,
                        x.MenuId
                    };

            foreach (var x in GetAllQuery)
            {
                RoleMenuEntity roleEntity = new()
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

            int TotalRoleMenu = await _context.DbSetRoleMenu.CountAsync();

            var paginatedRoleMenu = await _context.DbSetRoleMenu
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
        #endregion

        #region Non-Queries
        public async Task<bool> Add(RoleMenuEntity roleEntity, 
            CancellationToken cancellationToken)
        {
            await _context.DbSetRoleMenu
                .AddAsync(roleEntity, cancellationToken);

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> Update(RoleMenuEntity roleEntity, 
            CancellationToken cancellationToken)
        {
            _context.DbSetRoleMenu
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
