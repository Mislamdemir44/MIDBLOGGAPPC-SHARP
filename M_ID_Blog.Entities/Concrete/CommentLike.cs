using M_ID_Blog.Core.Entities;

namespace M_ID_Blog.Entities.Concrete
{
    public class CommentLike : IEntity
    {
        public int Id { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        // Foreign keys
        public string UserId { get; set; } = null!;
        public int CommentId { get; set; }
        
        // Navigation properties
        public virtual AppUser User { get; set; } = null!;
        public virtual Comment Comment { get; set; } = null!;
    }
}