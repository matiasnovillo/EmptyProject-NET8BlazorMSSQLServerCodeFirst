using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using EmptyProject.Areas.Testing.Entities;
using EmptyProject.Areas.BasicCore.Entities.Configuration;
using EmptyProject.Areas.Testing.DTOs;
using EmptyProject.Areas.Testing.Interfaces;

namespace EmptyProject.Areas.Testing.Repositories
{
    public class TestRepository : ITestRepository
    {
        protected readonly EFCoreContext _context;

        public TestRepository(EFCoreContext context)
        {
            _context = context;
        }

        public IQueryable<Test> AsQueryable()
        {
            try
            {
                return _context.Test.AsQueryable();
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
                return await _context.Test.CountAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Test?> GetByTestId(int testId, CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Test
                                .FirstOrDefaultAsync(x => x.TestId == testId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Test?>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Test.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<paginatedTestDTO> GetAllByTestIdPaginated(string textToSearch,
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

                int TotalTest = await _context.Test.CountAsync();

                var paginatedTest = await _context.Test
                        .Where(x => strictSearch ?
                            words.All(word => x.TestId.ToString().Contains(word)) :
                            words.Any(word => x.TestId.ToString().Contains(word)))
                        .OrderBy(p => p.TestId)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

                return new paginatedTestDTO
                {
                    lstTest = paginatedTest,
                    TotalItems = TotalTest,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Non-Queries
        public async Task<bool> Add(Test test, 
            CancellationToken cancellationToken)
        {
            try
            {
                await _context.Test.AddAsync(test, cancellationToken);
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(Test test, 
            CancellationToken cancellationToken)
        {
            try
            {
                _context.Test.Update(test);
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteByTestId(int testId, 
            CancellationToken cancellationToken)
        {
            try
            {
                AsQueryable()
                        .Where(x => x.TestId == testId)
                        .ExecuteDelete();

                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
