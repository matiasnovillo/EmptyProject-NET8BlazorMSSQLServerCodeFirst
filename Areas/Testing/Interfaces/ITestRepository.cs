using EmptyProject.Areas.Testing.Entities;
using EmptyProject.Areas.Testing.DTOs;

namespace EmptyProject.Areas.Testing.Interfaces
{
    public interface ITestRepository
    {
        IQueryable<Test> AsQueryable();

        #region Queries
        Task<int> Count(CancellationToken cancellationToken);

        Task<Test?> GetByTestId(int testId, CancellationToken cancellationToken);

        Task<List<Test?>> GetAll(CancellationToken cancellationToken);

        Task<paginatedTestDTO> GetAllByTestIdPaginated(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken);
        #endregion

        #region Non-Queries
        Task<bool> Add(Test test, CancellationToken cancellationToken);

        Task<bool> Update(Test test, CancellationToken cancellationToken);

        Task<bool> DeleteByTestId(int testId, CancellationToken cancellationToken);
        #endregion
    }
}
