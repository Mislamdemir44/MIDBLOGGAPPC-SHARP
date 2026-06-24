using M_ID_Blog.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace M_ID_Blog.Entities.Concrete
{
    public class Post : IEntity
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = null!;
        
        [Required]
        public string Content { get; set; } = null!;
        
        [StringLength(255)]
        public string? ImagePath { get; set; }
        
        [StringLength(255)]
        public string? VideoPath { get; set; }
        
        public int ViewCount { get; set; } = 0;
        
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        public DateTime? ModifiedDate { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        // Foreign keys
        public string UserId { get; set; } = null!;
        public int CategoryId { get; set; }
        
        // Navigation properties
        public virtual AppUser User { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}