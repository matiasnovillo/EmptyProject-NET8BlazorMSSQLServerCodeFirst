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
    public class ParameterRepository : IParameterRepository
    {
        protected readonly EFCoreContext _context;

        public ParameterRepository(EFCoreContext context)
        {
            _context = context;
        }

        public IQueryable<Parameter> AsQueryable()
        {
            try
            {
                return _context.Parameter.AsQueryable();
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
                return await _context.Parameter.CountAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Parameter?> GetByParameterId(int parameterId, CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Parameter
                                .FirstOrDefaultAsync(x => x.ParameterId == parameterId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Parameter?>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Parameter.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<paginatedParameterDTO> GetAllByParameterIdPaginated(string textToSearch,
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

                int TotalParameter = await _context.Parameter.CountAsync();

                var paginatedParameter = await _context.Parameter
                        .Where(x => strictSearch ?
                            words.All(word => x.ParameterId.ToString().Contains(word)) :
                            words.Any(word => x.ParameterId.ToString().Contains(word)))
                        .OrderBy(p => p.ParameterId)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

                return new paginatedParameterDTO
                {
                    lstParameter = paginatedParameter,
                    TotalItems = TotalParameter,
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
        public async Task<bool> Add(Parameter parameter, 
            CancellationToken cancellationToken)
        {
            try
            {
                await _context.Parameter.AddAsync(parameter, cancellationToken);
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(Parameter parameter, 
            CancellationToken cancellationToken)
        {
            try
            {
                _context.Parameter.Update(parameter);
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteByParameterId(int parameterId, 
            CancellationToken cancellationToken)
        {
            try
            {
                AsQueryable()
                        .Where(x => x.ParameterId == parameterId)
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
                List<Parameter> lstParameter = await _context.Parameter.ToListAsync(cancellationToken);

                DataTable DataTable = new();
                DataTable.Columns.Add("ParameterId", typeof(string));
                DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("Name", typeof(string));
                DataTable.Columns.Add("Value", typeof(string));
                DataTable.Columns.Add("IsPrivate", typeof(string));
                

                foreach (Parameter parameter in lstParameter)
                {
                    DataTable.Rows.Add(
                        parameter.ParameterId,
                        parameter.Active,
                        parameter.DateTimeCreation,
                        parameter.DateTimeLastModification,
                        parameter.UserCreationId,
                        parameter.UserLastModificationId,
                        parameter.Name,
                        parameter.Value,
                        parameter.IsPrivate
                        
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
