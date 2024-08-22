using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, List<string> includes = null);
        Task<List<T>> GetAllAsync(List<string> includes = null);

        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, List<string> includes = null);

        Task<int> AddAsync(T entity);

        Task<int> UpdateAsync(T entity);
        Task<int> RemoveAsync(T entity);

        void BulkInsert(IEnumerable<T> entities);

        void BulkUpdate(IEnumerable<T> entities);
        void BulkDelete(IEnumerable<T> entities);
    }
}
