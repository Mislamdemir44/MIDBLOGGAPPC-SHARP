using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace M_ID_Blog.Entities.Concrete
{
    public class AppUser : IdentityUser
    {
        [StringLength(100)]
        public string? FullName { get; set; }
        
        [StringLength(500)]
        public string? Biography { get; set; }
        
        [StringLength(255)]
        public string? ProfileImagePath { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        public bool IsActive { get; set; } = true;
        
        // Navigation properties
        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public virtual ICollection<CommentLike> CommentLikes { get; set; } = new HashSet<CommentLike>();
    }
}