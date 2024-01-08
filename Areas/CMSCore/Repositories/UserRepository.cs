using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using EmptyProject.Areas.CMSCore.Entities;
using EmptyProject.Areas.BasicCore.Entities.Configuration;
using EmptyProject.Areas.CMSCore.DTOs;
using EmptyProject.Areas.CMSCore.Interfaces;
using System.Data;

/*
 * GUID:e6c09dfe-3a3e-461b-b3f9-734aee05fc7b
 * 
 * Coded by fiyistack.com
 * Copyright © 2024
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

namespace EmptyProject.Areas.CMSCore.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly EFCoreContext _context;

        public UserRepository(EFCoreContext context)
        {
            _context = context;
        }

        public IQueryable<User> AsQueryable()
        {
            try
            {
                return _context.User.AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Queries
        public async Task<int> Count(CancellationToken cancellationToken)
        {
            try
            {
                return await _context.User.CountAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User?> GetByUserId(int userId, CancellationToken cancellationToken)
        {
            try
            {
                return await _context.User
                                .FirstOrDefaultAsync(x => x.UserId == userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<User?>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                return await _context.User.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<paginatedUserDTO> GetAllByUserIdPaginated(string textToSearch,
            bool strictSearch,
            int pageIndex, 
            int pageSize,
            CancellationToken cancellationToken)
        {
            try
            {
                //textToSearch: "novillo matias  com" -> words: {novillo,matias,com}
                string[] words = Regex
                    .Replace(textToSearch
                    .Trim(), @"\s+", " ")
                    .Split(" ");

                int TotalUser = await _context.User.CountAsync();

                var paginatedUser = await _context.User
                        .Where(x => strictSearch ?
                            words.All(word => x.UserId.ToString().Contains(word)) :
                            words.Any(word => x.UserId.ToString().Contains(word)))
                        .OrderBy(p => p.UserId)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

                return new paginatedUserDTO
                {
                    lstUser = paginatedUser,
                    TotalItems = TotalUser,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<User?> GetAllByEmail(string textToSearch,
    bool strictSearch,
    CancellationToken cancellationToken)
        {
            //textToSearch: "novillo matias  com" -> words: {novillo,matias,com}
            string[] words = Regex
                .Replace(textToSearch
                .Trim(), @"\s+", " ")
                .Split(" ");

            List<User?> lstUser = [];

            var GetAllQuery = AsQueryable()
                .Where(x => strictSearch ?
                    words.All(word => x.Email.Contains(word)) :
                    words.Any(word => x.Email.Contains(word)))
                .ToList();

            foreach (var x in GetAllQuery)
            {
                User user = new()
                {
                    UserId = x.UserId,
                    Email = x.Email,
                    Password = x.Password
                };
                lstUser.Add(user);
            }

            return lstUser;
        }

        public async Task<User?> GetByEmailAndPassword(string email,
            string password,
            CancellationToken cancellationToken)
        {
            return await _context.User.AsQueryable()
                .Where(u => u.Password == password)
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();
        }
        #endregion

        #region Non-Queries
        public async Task<bool> Add(User user, 
            CancellationToken cancellationToken)
        {
            try
            {
                await _context.User.AddAsync(user, cancellationToken);
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(User user, 
            CancellationToken cancellationToken)
        {
            try
            {
                _context.User.Update(user);
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteByUserId(int userId, 
            CancellationToken cancellationToken)
        {
            try
            {
                AsQueryable()
                        .Where(x => x.UserId == userId)
                        .ExecuteDelete();

                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Other methods
        public async Task<DataTable> GetAllInDataTable(CancellationToken cancellationToken)
        {
            try
            {
                List<User> lstUser = await _context.User.ToListAsync(cancellationToken);

                DataTable DataTable = new();
                DataTable.Columns.Add("UserId", typeof(string));
                DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("Email", typeof(string));
                DataTable.Columns.Add("Password", typeof(string));
                DataTable.Columns.Add("RoleId", typeof(string));
                

                foreach (User user in lstUser)
                {
                    DataTable.Rows.Add(
                        user.UserId,
                        user.Active,
                        user.DateTimeCreation,
                        user.DateTimeLastModification,
                        user.UserCreationId,
                        user.UserLastModificationId,
                        user.Email,
                        user.Password,
                        user.RoleId
                        
                        );
                }

                return DataTable;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
