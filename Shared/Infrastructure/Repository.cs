using Microsoft.EntityFrameworkCore;
using Shared.Dto;
using Shared.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shared.Infrastructure
{
    public abstract class Repository<T> : IRepository<T> where T : class, IAggregateRoot
    {
        protected DbContext DbContext { get; }
        protected DbSet<T> DbSet => DbContext.Set<T>();

        protected Repository(DbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
        }
        public async Task<QueryResult<T>> QueryAsync(Expression<Func<T, bool>> predicate, int skip, int take)
        {
            var queryable = DbContext.Set<T>().Where(predicate);
            return await queryable.ToQueryResultAsync(skip, take);
        }

        public async Task<IList<T>> GetManyAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IList<T>> GetManyAsync(Expression<Func<T, bool>> predicate, int skip, int take)
        {
            return await DbContext.Set<T>().Where(predicate)                  
                        .Skip(skip)
                        .Take(take)
                        .ToListAsync();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await DbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<T> GetSingleAsync()
        {
            return await DbContext.Set<T>().FirstOrDefaultAsync();
        }

        public async Task<T> GetByIdAsync(Guid entityId)
        {
            return await DbContext.Set<T>().FindAsync(entityId);
        }

        public async Task<T> GetByIdAsync<Tkey>(Tkey entityId)
        {
            return await DbContext.Set<T>().FindAsync(entityId);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbContext.Set<T>().AnyAsync(predicate);
        }

        public virtual void Add(T entity)
        {
            DbContext.Set<T>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            DbContext.Set<T>().AddRange(entities);
        }

        public void Update(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(Guid entityId)
        {
            var entity = DbContext.Find<T>(entityId);
            var t = entity as Entity;
            DbContext.Entry(entity).State = EntityState.Deleted;
        }

        public virtual void DeleteMany(IEnumerable<T> entities)
        {
            DbContext.Set<T>().RemoveRange(entities);
        }
        public async Task<int> CountAllAsync()
        {
            return await DbContext.Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbContext.Set<T>().CountAsync(predicate);
        }
    }
}
