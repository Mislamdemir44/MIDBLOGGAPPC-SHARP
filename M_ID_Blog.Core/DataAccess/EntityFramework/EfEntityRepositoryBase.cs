using M_ID_Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace M_ID_Blog.Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext
    {
        protected readonly TContext _context;

        public EfEntityRepositoryBase(TContext context)
        {
            _context = context;
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            query = query.Where(filter);
            
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            
            if (filter != null)
            {
                query = query.Where(filter);
            }
            
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            
            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            
            return await query.ToListAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _context.Set<TEntity>().AnyAsync(filter);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            
            if (filter != null)
            {
                query = query.Where(filter);
            }
            
            return await query.CountAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => _context.Set<TEntity>().Update(entity));
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => _context.Set<TEntity>().Remove(entity));
        }
    }
}