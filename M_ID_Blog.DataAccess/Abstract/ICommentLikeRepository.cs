using M_ID_Blog.Core.DataAccess;
using M_ID_Blog.Entities.Concrete;

namespace M_ID_Blog.DataAccess.Abstract
{
    public interface ICommentLikeRepository : IEntityRepository<CommentLike>
    {
        Task<CommentLike?> GetByCommentAndUserIdAsync(int commentId, string userId);
    }
}