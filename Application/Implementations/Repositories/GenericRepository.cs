using Application;
using Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly AppDbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<int> AddAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
            var response = await _dbContext.SaveChangesAsync();
            return response;

        }

        public void BulkDelete(IEnumerable<T> entities)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                _dbContext.RemoveRange(entities);
                transaction.Commit();
            }
        }

        public void BulkInsert(IEnumerable<T> entities)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                _dbContext.AddRange(entities);
                transaction.Commit();
            }
        }

        public void BulkUpdate(IEnumerable<T> entities)
        {

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                _dbContext.UpdateRange(entities);
                transaction.Commit();
            }
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, List<string> includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.Where(predicate).ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(List<string> includes = null)
        {

            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, List<string> includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<int> RemoveAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            var response = await _dbContext.SaveChangesAsync();
            return response;
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync();
        }
    }
}
