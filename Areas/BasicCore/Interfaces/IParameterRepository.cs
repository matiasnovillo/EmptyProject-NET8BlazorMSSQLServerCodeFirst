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
    public interface IParameterRepository
    {
        IQueryable<Parameter> AsQueryable();

        #region Queries
        Task<int> Count(CancellationToken cancellationToken);

        Task<Parameter?> GetByParameterId(int testId, CancellationToken cancellationToken);

        Task<List<Parameter?>> GetAll(CancellationToken cancellationToken);

        Task<paginatedParameterDTO> GetAllByParameterIdPaginated(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken);
        #endregion

        #region Non-Queries
        Task<bool> Add(Parameter test, CancellationToken cancellationToken);

        Task<bool> Update(Parameter test, CancellationToken cancellationToken);

        Task<bool> DeleteByParameterId(int testId, CancellationToken cancellationToken);
        #endregion

        #region Other methods
        Task<DataTable> GetAllInDataTable(CancellationToken cancellationToken);
        #endregion
    }
}
