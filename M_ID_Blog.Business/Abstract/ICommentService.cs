using M_ID_Blog.Business.DTOs.Comment;

namespace M_ID_Blog.Business.Abstract
{
    public interface ICommentService
    {
        Task<CommentDto?> GetByIdAsync(int commentId);
        Task<IList<CommentListDto>> GetAllByPostIdAsync(int postId);
        Task<IList<CommentListDto>> GetAllByUserIdAsync(string userId);
        Task<CommentDto> AddAsync(CommentAddDto commentAddDto);
        Task<CommentDto> AddReplyAsync(CommentReplyDto commentReplyDto);
        Task<CommentDto> UpdateAsync(CommentUpdateDto commentUpdateDto);
        Task DeleteAsync(int commentId);
        Task<bool> LikeCommentAsync(int commentId, string userId);
        Task<bool> UnlikeCommentAsync(int commentId, string userId);
    }
}