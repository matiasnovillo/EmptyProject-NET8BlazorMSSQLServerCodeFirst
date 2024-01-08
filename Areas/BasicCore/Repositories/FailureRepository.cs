using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using EmptyProject.Areas.BasicCore.Entities;
using EmptyProject.Areas.BasicCore.Entities.Configuration;
using EmptyProject.Areas.BasicCore.DTOs;
using EmptyProject.Areas.BasicCore.Interfaces;
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

namespace EmptyProject.Areas.BasicCore.Repositories
{
    public class FailureRepository : IFailureRepository
    {
        protected readonly EFCoreContext _context;

        public FailureRepository(EFCoreContext context)
        {
            _context = context;
        }

        public IQueryable<Failure> AsQueryable()
        {
            try
            {
                return _context.Failure.AsQueryable();
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
                return await _context.Failure.CountAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Failure?> GetByFailureId(int failureId, CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Failure
                                .FirstOrDefaultAsync(x => x.FailureId == failureId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Failure?>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Failure.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<paginatedFailureDTO> GetAllByFailureIdPaginated(string textToSearch,
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

                int TotalFailure = await _context.Failure.CountAsync();

                var paginatedFailure = await _context.Failure
                        .Where(x => strictSearch ?
                            words.All(word => x.FailureId.ToString().Contains(word)) :
                            words.Any(word => x.FailureId.ToString().Contains(word)))
                        .OrderBy(p => p.FailureId)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

                return new paginatedFailureDTO
                {
                    lstFailure = paginatedFailure,
                    TotalItems = TotalFailure,
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
        public async Task<bool> Add(Failure failure, 
            CancellationToken cancellationToken)
        {
            try
            {
                await _context.Failure.AddAsync(failure, cancellationToken);
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(Failure failure, 
            CancellationToken cancellationToken)
        {
            try
            {
                _context.Failure.Update(failure);
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteByFailureId(int failureId, 
            CancellationToken cancellationToken)
        {
            try
            {
                AsQueryable()
                        .Where(x => x.FailureId == failureId)
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
                List<Failure> lstFailure = await _context.Failure.ToListAsync(cancellationToken);

                DataTable DataTable = new();
                DataTable.Columns.Add("FailureId", typeof(string));
                DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("Message", typeof(string));
                DataTable.Columns.Add("EmergencyLevel", typeof(string));
                DataTable.Columns.Add("StackTrace", typeof(string));
                DataTable.Columns.Add("Source", typeof(string));
                DataTable.Columns.Add("Comment", typeof(string));
                

                foreach (Failure failure in lstFailure)
                {
                    DataTable.Rows.Add(
                        failure.FailureId,
                        failure.Active,
                        failure.DateTimeCreation,
                        failure.DateTimeLastModification,
                        failure.UserCreationId,
                        failure.UserLastModificationId,
                        failure.Message,
                        failure.EmergencyLevel,
                        failure.StackTrace,
                        failure.Source,
                        failure.Comment
                        
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
