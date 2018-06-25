using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CrossSolar.Domain;
using Microsoft.EntityFrameworkCore;

namespace CrossSolar.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
        where T : class, new()
    {
        protected CrossSolarDbContext DbContext { get; set; }

        public async Task<T> GetAsync(string id)
        {
            return await DbContext.FindAsync<T>(id);
        }

        public Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return DbContext.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            return DbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public IQueryable<T> Query()
        {
            return DbContext.Set<T>().AsQueryable();
        }

        public async Task InsertAsync(T entity)
        {
            DbContext.Set<T>().Add(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
        }
    }
}