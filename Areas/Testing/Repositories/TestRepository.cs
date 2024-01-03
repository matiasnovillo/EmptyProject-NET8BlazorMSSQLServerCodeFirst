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
            return _context.Test.AsQueryable();
        }

        #region Queries
        public async Task<int> Count(CancellationToken cancellationToken)
        {
            return await _context.Test.CountAsync();
        }

        public Test? GetByTestId(int testId, CancellationToken cancellationToken)
        {
            return _context.Test.FirstOrDefault(x => x.TestId == testId);
        }

        public List<Test?> GetAll(CancellationToken cancellationToken)
        {
            List<Test?> lstTest = [];

            var GetAllQuery =
                    from x in _context.Test
                    select new
                    {
                        x.TestId,
                        x.Boolean,
                        x.DateTime,
                        x.Decimal,
                        x.ForeignKeyDropdown,
                        x.ForeignKeyOptions,
                        x.Integer,
                        x.Basic,
                        x.Email,
                        x.File,
                        x.HexColour,
                        x.Password,
                        x.PhoneNumber,
                        x.Tag,
                        x.TextArea,
                        x.TextEditor,
                        x.URL,
                        x.Time
                    };

            foreach (var x in GetAllQuery)
            {
                Test test = new()
                {
                    TestId = x.TestId,
                    Boolean = x.Boolean,
                    DateTime = x.DateTime,
                    Decimal = x.Decimal,
                    ForeignKeyDropdown = x.ForeignKeyDropdown,
                    ForeignKeyOptions = x.ForeignKeyOptions,
                    Integer = x.Integer,
                    Basic = x.Basic,
                    Email = x.Email,
                    File = x.File,
                    HexColour = x.HexColour,
                    Password = x.Password,
                    PhoneNumber = x.PhoneNumber,
                    Tag = x.Tag,
                    TextArea = x.TextArea,
                    TextEditor = x.TextEditor,
                    URL = x.URL,
                    Time = x.Time
                };
                lstTest.Add(test);
            }

            return lstTest;
        }

        public async Task<paginatedTestDTO> GetAllByTestIdPaginated(string textToSearch,
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
        #endregion

        #region Non-Queries
        public async Task<bool> Add(Test test, 
            CancellationToken cancellationToken)
        {
            await _context.Test.AddAsync(test, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> Update(Test test, 
            CancellationToken cancellationToken)
        {
            _context.Test.Update(test);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteByTestId(int testId, 
            CancellationToken cancellationToken)
        {
            AsQueryable()
                .Where(x => x.TestId == testId)
                .ExecuteDelete();

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
        #endregion
    }
}
