using M_ID_Blog.Core.DataAccess;
using M_ID_Blog.Entities.Concrete;

namespace M_ID_Blog.DataAccess.Abstract
{
    public interface IPostRepository : IEntityRepository<Post>
    {
        Task<IList<Post>> GetAllByUserIdAsync(string userId);
        Task<IList<Post>> GetAllByCategoryIdAsync(int categoryId);
        Task<IList<Post>> GetAllWithDetailsAsync();
        Task IncrementViewCountAsync(int postId);
    }
}