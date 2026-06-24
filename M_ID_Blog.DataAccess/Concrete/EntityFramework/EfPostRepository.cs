using M_ID_Blog.Core.DataAccess.EntityFramework;
using M_ID_Blog.DataAccess.Abstract;
using M_ID_Blog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace M_ID_Blog.DataAccess.Concrete.EntityFramework
{
    public class EfPostRepository : EfEntityRepositoryBase<Post, MidBlogContext>, IPostRepository
    {
        public EfPostRepository(MidBlogContext context) : base(context)
        {
        }

        public async Task<IList<Post>> GetAllByUserIdAsync(string userId)
        {
            return await GetAllAsync(p => p.UserId == userId && p.IsActive, 
                null, p => p.Category, p => p.User);
        }

        public async Task<IList<Post>> GetAllByCategoryIdAsync(int categoryId)
        {
            return await GetAllAsync(p => p.CategoryId == categoryId && p.IsActive,
                query => query.OrderByDescending(p => p.CreatedDate),
                p => p.Category, p => p.User);
        }

        public async Task<IList<Post>> GetAllWithDetailsAsync()
        {
            return await GetAllAsync(p => p.IsActive,
                query => query.OrderByDescending(p => p.CreatedDate),
                p => p.Category, p => p.User);
        }

        public async Task IncrementViewCountAsync(int postId)
        {
            var post = await GetAsync(p => p.Id == postId);
            if (post != null)
            {
                post.ViewCount++;
                await UpdateAsync(post);
            }
        }
    }
}