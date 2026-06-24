using M_ID_Blog.Core.DataAccess.UnitOfWork;
using M_ID_Blog.DataAccess.Abstract;
using M_ID_Blog.DataAccess.Concrete.EntityFramework;

namespace M_ID_Blog.DataAccess.Concrete.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MidBlogContext _context;
        private EfCategoryRepository? _categoryRepository;
        private EfPostRepository? _postRepository;
        private EfCommentRepository? _commentRepository;
        private EfCommentLikeRepository? _commentLikeRepository;

        public UnitOfWork(MidBlogContext context)
        {
            _context = context;
        }

        public ICategoryRepository Categories => _categoryRepository ??= new EfCategoryRepository(_context);
        public IPostRepository Posts => _postRepository ??= new EfPostRepository(_context);
        public ICommentRepository Comments => _commentRepository ??= new EfCommentRepository(_context);
        public ICommentLikeRepository CommentLikes => _commentLikeRepository ??= new EfCommentLikeRepository(_context);

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}