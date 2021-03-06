using System.Linq.Expressions;
using AestheticLife.DataAccess.Domain.Abstractions.Interfaces;
using AestheticsLife.DataAccess.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AestheticLife.DataAccess;

internal class BaseReadonlyRepository<TEntity>
        : IBaseReadonlyRepository<TEntity> 
        where TEntity : class
    {
        protected DbContext _dbContext;
        protected DbSet<TEntity> _dbSet;

        public BaseReadonlyRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public virtual IEnumerable<TEntity> GetNoTracking(
            Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.AsNoTracking().ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetNoTrackingAsync(
            Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            params Expression<Func<TEntity,object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public virtual IEnumerable<TEntity> GetNoTracking(
            Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.AsNoTracking().ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            params Expression<Func<TEntity,object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetNoTrackingAsync(
            Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.AsNoTracking().ToListAsync();
        }
    }