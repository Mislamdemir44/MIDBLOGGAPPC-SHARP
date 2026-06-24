using M_ID_Blog.Core.DataAccess.EntityFramework;
using M_ID_Blog.DataAccess.Abstract;
using M_ID_Blog.Entities.Concrete;

namespace M_ID_Blog.DataAccess.Concrete.EntityFramework
{
    public class EfCommentLikeRepository : EfEntityRepositoryBase<CommentLike, MidBlogContext>, ICommentLikeRepository
    {
        public EfCommentLikeRepository(MidBlogContext context) : base(context)
        {
        }

        public async Task<CommentLike?> GetByCommentAndUserIdAsync(int commentId, string userId)
        {
            return await GetAsync(cl => cl.CommentId == commentId && cl.UserId == userId);
        }
    }
}