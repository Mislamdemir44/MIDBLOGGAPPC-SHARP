using M_ID_Blog.Business.DTOs.Post;

namespace M_ID_Blog.Business.Abstract
{
    public interface IPostService
    {
        Task<PostDto?> GetByIdAsync(int postId);
        Task<IList<PostListDto>> GetAllAsync();
        Task<IList<PostListDto>> GetAllByUserIdAsync(string userId);
        Task<IList<PostListDto>> GetAllByCategoryIdAsync(int categoryId);
        Task<PostDto> AddAsync(PostAddDto postAddDto);
        Task<PostDto> UpdateAsync(PostUpdateDto postUpdateDto);
        Task DeleteAsync(int postId);
        Task IncrementViewCountAsync(int postId);
    }
}