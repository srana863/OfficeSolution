using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layer.Data.Interfaces.Common
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
