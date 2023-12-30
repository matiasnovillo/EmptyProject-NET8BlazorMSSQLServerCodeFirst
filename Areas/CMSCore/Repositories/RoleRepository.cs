using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using EmptyProject.Areas.CMSCore.DTOs;
using EmptyProject.Areas.CMSCore.Entities;
using EmptyProject.Areas.BasicCore.Entities.Configuration;

namespace EmptyProject.Areas.CMSCore.Repositories
{
    public class RoleRepository
    {
        protected readonly EFCoreContext _context;

        public RoleRepository(EFCoreContext context)
        {
            _context = context;
        }

        public IQueryable<RoleEntity> AsQueryable()
        {
            return _context.DbSetRole
                        .AsQueryable();
        }

        #region Queries
        public async Task<int> Count(CancellationToken cancellationToken)
        {
            return await _context.DbSetRole
                            .CountAsync();
        }

        public RoleEntity? GetById(int roleId, CancellationToken cancellationToken)
        {
            return _context.DbSetRole
                        .FirstOrDefault(x => x.RoleId == roleId);
        }

        public List<RoleEntity?> GetAll(CancellationToken cancellationToken)
        {
            List<RoleEntity?> lstRole = [];

            var GetAllQuery =
                    from x in _context.DbSetRole
                    select new
                    {
                        x.RoleId,
                        x.Name
                    };

            foreach (var x in GetAllQuery)
            {
                RoleEntity roleEntity = new()
                {
                    RoleId = x.RoleId,
                    Name = x.Name
                };
                lstRole.Add(roleEntity);
            }

            return lstRole;
        }

        public async Task<paginatedRoleDTO> GetAllByRoleIdPaginated(string textToSearch,
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

            int TotalRole = await _context.DbSetRole.CountAsync();

            var paginatedRole = await _context.DbSetRole
                    .Where(x => strictSearch ?
                        words.All(word => x.RoleId.ToString().Contains(word)) :
                        words.Any(word => x.RoleId.ToString().Contains(word)))
                    .OrderBy(p => p.RoleId)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

            return new paginatedRoleDTO
            {
                lstRole = paginatedRole,
                TotalItems = TotalRole,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }
        #endregion

        #region Non-Queries
        public async Task<bool> Add(RoleEntity roleEntity, 
            CancellationToken cancellationToken)
        {
            await _context.DbSetRole
                .AddAsync(roleEntity, cancellationToken);

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> Update(RoleEntity roleEntity, 
            CancellationToken cancellationToken)
        {
            _context.DbSetRole
                .Update(roleEntity);

            return await _context
                        .SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteByRoleId(int roleId, 
            CancellationToken cancellationToken)
        {
            AsQueryable()
                .Where(x => x.RoleId == roleId)
                .ExecuteDelete();

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
        #endregion
    }
}
