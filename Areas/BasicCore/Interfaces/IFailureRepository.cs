using EmptyProject.Areas.BasicCore.Entities;
using EmptyProject.Areas.BasicCore.DTOs;
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

namespace EmptyProject.Areas.BasicCore.Interfaces
{
    public interface IFailureRepository
    {
        IQueryable<Failure> AsQueryable();

        #region Queries
        Task<int> Count(CancellationToken cancellationToken);

        Task<Failure?> GetByFailureId(int testId, CancellationToken cancellationToken);

        Task<List<Failure?>> GetAll(CancellationToken cancellationToken);

        Task<paginatedFailureDTO> GetAllByFailureIdPaginated(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken);
        #endregion

        #region Non-Queries
        Task<bool> Add(Failure test, CancellationToken cancellationToken);

        Task<bool> Update(Failure test, CancellationToken cancellationToken);

        Task<bool> DeleteByFailureId(int testId, CancellationToken cancellationToken);
        #endregion

        #region Other methods
        Task<DataTable> GetAllInDataTable(CancellationToken cancellationToken);
        #endregion
    }
}
