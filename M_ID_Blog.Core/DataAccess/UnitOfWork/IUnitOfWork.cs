namespace M_ID_Blog.Core.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<int> SaveAsync();
    }
}