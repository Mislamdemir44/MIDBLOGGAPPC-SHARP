using M_ID_Blog.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace M_ID_Blog.Entities.Concrete
{
    public class Comment : IEntity
    {
        public int Id { get; set; }
        
        [Required]
        public string Content { get; set; } = null!;
        
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        public DateTime? ModifiedDate { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        // Foreign keys
        public string UserId { get; set; } = null!;
        public int PostId { get; set; }
        public int? ParentCommentId { get; set; }
        
        // Navigation properties
        public virtual AppUser User { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
        public virtual Comment? ParentComment { get; set; }
        public virtual ICollection<Comment> Replies { get; set; } = new HashSet<Comment>();
        public virtual ICollection<CommentLike> Likes { get; set; } = new HashSet<CommentLike>();
    }
}