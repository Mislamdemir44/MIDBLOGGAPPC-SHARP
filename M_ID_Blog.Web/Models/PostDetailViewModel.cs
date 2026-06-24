using M_ID_Blog.Business.DTOs.Comment;
using M_ID_Blog.Business.DTOs.Post;

namespace M_ID_Blog.Web.Models
{
    public class PostDetailViewModel
    {
        public PostDto Post { get; set; } = null!;
        public CommentAddDto NewComment { get; set; } = new CommentAddDto();
        public string? CurrentUserId { get; set; }
    }
}