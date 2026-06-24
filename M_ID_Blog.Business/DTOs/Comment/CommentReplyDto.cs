using System.ComponentModel.DataAnnotations;

namespace M_ID_Blog.Business.DTOs.Comment
{
    public class CommentReplyDto
    {
        [Required(ErrorMessage = "Reply content is required")]
        public string Content { get; set; } = null!;
        
        [Required]
        public int PostId { get; set; }
        
        [Required]
        public int ParentCommentId { get; set; }
        
        public string UserId { get; set; } = null!;
    }
}