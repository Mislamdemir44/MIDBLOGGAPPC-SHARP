using M_ID_Blog.Core.DataAccess;
using M_ID_Blog.Entities.Concrete;

namespace M_ID_Blog.DataAccess.Abstract
{
    public interface ICommentRepository : IEntityRepository<Comment>
    {
        Task<IList<Comment>> GetAllByPostIdAsync(int postId);
        Task<IList<Comment>> GetAllByUserIdAsync(string userId);
        Task<int> GetCommentLikesCountAsync(int commentId);
        Task<bool> IsCommentLikedByUserAsync(int commentId, string userId);
    }
}