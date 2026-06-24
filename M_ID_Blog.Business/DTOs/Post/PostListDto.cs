using M_ID_Blog.Business.DTOs.Category;
using M_ID_Blog.Business.DTOs.User;

namespace M_ID_Blog.Business.DTOs.Post
{
    public class PostListDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? ImagePath { get; set; }
        public int ViewCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CommentCount { get; set; }
        
        public UserDto User { get; set; } = null!;
        public CategoryDto Category { get; set; } = null!;
    }
}