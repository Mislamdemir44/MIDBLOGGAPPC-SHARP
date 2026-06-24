using M_ID_Blog.Core.DataAccess.EntityFramework;
using M_ID_Blog.DataAccess.Abstract;
using M_ID_Blog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace M_ID_Blog.DataAccess.Concrete.EntityFramework
{
    public class EfCommentRepository : EfEntityRepositoryBase<Comment, MidBlogContext>, ICommentRepository
    {
        public EfCommentRepository(MidBlogContext context) : base(context)
        {
        }

        public async Task<IList<Comment>> GetAllByPostIdAsync(int postId)
        {
            return await GetAllAsync(c => c.PostId == postId && c.IsActive && c.ParentCommentId == null,
                query => query.OrderByDescending(c => c.CreatedDate),
                c => c.User, c => c.Likes, c => c.Replies);
        }

        public async Task<IList<Comment>> GetAllByUserIdAsync(string userId)
        {
            return await GetAllAsync(c => c.UserId == userId && c.IsActive,
                query => query.OrderByDescending(c => c.CreatedDate),
                c => c.Post, c => c.Likes);
        }

        public async Task<int> GetCommentLikesCountAsync(int commentId)
        {
            return await _context.CommentLikes.CountAsync(cl => cl.CommentId == commentId);
        }

        public async Task<bool> IsCommentLikedByUserAsync(int commentId, string userId)
        {
            return await _context.CommentLikes.AnyAsync(cl => cl.CommentId == commentId && cl.UserId == userId);
        }
    }
}