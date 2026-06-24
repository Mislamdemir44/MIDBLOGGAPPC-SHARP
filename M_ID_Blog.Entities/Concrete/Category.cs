using M_ID_Blog.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace M_ID_Blog.Entities.Concrete
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        public bool IsActive { get; set; } = true;
        
        // Navigation properties
        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    }
}