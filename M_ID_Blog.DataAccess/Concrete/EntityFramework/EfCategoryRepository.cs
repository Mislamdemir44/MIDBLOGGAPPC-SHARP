using M_ID_Blog.Core.DataAccess.EntityFramework;
using M_ID_Blog.DataAccess.Abstract;
using M_ID_Blog.Entities.Concrete;

namespace M_ID_Blog.DataAccess.Concrete.EntityFramework
{
    public class EfCategoryRepository : EfEntityRepositoryBase<Category, MidBlogContext>, ICategoryRepository
    {
        public EfCategoryRepository(MidBlogContext context) : base(context)
        {
        }
    }
}