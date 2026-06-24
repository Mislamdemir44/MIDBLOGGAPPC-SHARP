using M_ID_Blog.Core.DataAccess;
using M_ID_Blog.Entities.Concrete;

namespace M_ID_Blog.DataAccess.Abstract
{
    public interface ICategoryRepository : IEntityRepository<Category>
    {
    }
}