using Microsoft.EntityFrameworkCore;
using EmptyProject.Entities;
using System.Text.RegularExpressions;
using EmptyProject.CommonFunctions.DTOs;

namespace EmptyProject.Repositories
{
    public class UserRepository
    {
        protected readonly Entities.Configuration.EFCoreContext _context;

        public UserRepository(Entities.Configuration.EFCoreContext context)
        {
            _context = context;
        }

        public IQueryable<User> AsQueryable()
        {
            return _context.User.AsQueryable();
        }

        #region Queries
        public async Task<int> Count(CancellationToken cancellationToken)
        {
            return await _context.User.CountAsync();
        }

        public User? GetById(int userId, CancellationToken cancellationToken)
        {
            return _context.User.FirstOrDefault(u => u.UserId == userId);
        }

        public List<User?> GetAll(CancellationToken cancellationToken)
        {
            List<User?> lstUser = [];

            var GetAllQuery =
                    from x in _context.User
                    select new
                    {
                        x.UserId,
                        x.Email,
                        x.Password,
                    };

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

        public User? GetByEmailAndPassword(string email, 
            string password, 
            CancellationToken cancellationToken)
        {
            return _context.User.AsQueryable()
                .Where(u => u.Password == password)
                .Where(u => u.Email == email)
                .FirstOrDefault();
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

        public async Task<paginatedUserDTO> GetAllByEmailPaginated(string textToSearch,
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

            int TotalUser = await _context.User.CountAsync();

            var paginatedUser = await _context.User
                    .Where(x => strictSearch ?
                        words.All(word => x.Email.Contains(word)) :
                        words.Any(word => x.Email.Contains(word)))
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
        #endregion

        #region Non-Queries
        public async Task<bool> Add(User user, 
            CancellationToken cancellationToken)
        {
            await _context.User.AddAsync(user, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> Update(User user, 
            CancellationToken cancellationToken)
        {
            _context.User.Update(user);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteByUserId(int userId, 
            CancellationToken cancellationToken)
        {
            AsQueryable()
                .Where(u => u.UserId == userId)
                .ExecuteDelete();

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
        #endregion
    }
}
