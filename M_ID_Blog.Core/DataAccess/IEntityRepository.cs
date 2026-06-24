using M_ID_Blog.Core.Entities;
using System.Linq.Expressions;

namespace M_ID_Blog.Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        Task<T?> GetAsync(Expression<Func<T, bool>> filter, 
            params Expression<Func<T, object>>[] includeProperties);
        
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            params Expression<Func<T, object>>[] includeProperties);
        
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
        
        Task<int> CountAsync(Expression<Func<T, bool>>? filter = null);
        
        Task AddAsync(T entity);
        
        Task UpdateAsync(T entity);
        
        Task DeleteAsync(T entity);
    }
}