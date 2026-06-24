using System.ComponentModel.DataAnnotations;

namespace M_ID_Blog.Business.DTOs.Comment
{
    public class CommentAddDto
    {
        [Required(ErrorMessage = "Comment content is required")]
        public string Content { get; set; } = null!;
        
        [Required]
        public int PostId { get; set; }
        
        public string UserId { get; set; } = null!;
    }
}