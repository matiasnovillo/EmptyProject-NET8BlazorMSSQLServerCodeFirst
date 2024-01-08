using EmptyProject.Areas.CMSCore.Entities;
using EmptyProject.Areas.CMSCore.DTOs;
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

namespace EmptyProject.Areas.CMSCore.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> AsQueryable();

        #region Queries
        Task<int> Count(CancellationToken cancellationToken);

        Task<User?> GetByUserId(int testId, CancellationToken cancellationToken);

        Task<List<User?>> GetAll(CancellationToken cancellationToken);

        Task<paginatedUserDTO> GetAllByUserIdPaginated(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken);
        #endregion

        #region Non-Queries
        Task<bool> Add(User test, CancellationToken cancellationToken);

        Task<bool> Update(User test, CancellationToken cancellationToken);

        Task<bool> DeleteByUserId(int testId, CancellationToken cancellationToken);
        #endregion

        #region Other methods
        Task<DataTable> GetAllInDataTable(CancellationToken cancellationToken);
        #endregion
    }
}
