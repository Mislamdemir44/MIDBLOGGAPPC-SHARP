using M_ID_Blog.Business.DTOs.Category;
using M_ID_Blog.Business.DTOs.Comment;
using M_ID_Blog.Business.DTOs.User;

namespace M_ID_Blog.Business.DTOs.Post
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? ImagePath { get; set; }
        public string? VideoPath { get; set; }
        public int ViewCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        
        public UserDto User { get; set; } = null!;
        public CategoryDto Category { get; set; } = null!;
        public IList<CommentListDto> Comments { get; set; } = new List<CommentListDto>();
    }
}