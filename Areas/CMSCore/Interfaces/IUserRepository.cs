using EmptyProject.Areas.CMSCore.Entities;
using EmptyProject.Areas.CMSCore.DTOs;

namespace EmptyProject.Areas.CMSCore.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> AsQueryable();

        #region Queries
        Task<int> Count(CancellationToken cancellationToken);
        User? GetByUserId(int userId, CancellationToken cancellationToken);
        List<User?> GetAll(CancellationToken cancellationToken);
        #endregion

        #region Non-Queries
        Task<bool> Add(User user, CancellationToken cancellationToken);
        Task<bool> Update(User user, CancellationToken cancellationToken);
        Task<bool> DeleteByUserId(int userId, CancellationToken cancellationToken);
        #endregion
    }
}
