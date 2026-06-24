using System.ComponentModel.DataAnnotations;

namespace M_ID_Blog.Business.DTOs.Comment
{
    public class CommentUpdateDto
    {
        [Required]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Comment content is required")]
        public string Content { get; set; } = null!;
        
        public string UserId { get; set; } = null!;
    }
}