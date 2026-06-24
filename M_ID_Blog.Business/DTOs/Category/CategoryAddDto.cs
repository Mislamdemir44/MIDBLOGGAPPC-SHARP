using System.ComponentModel.DataAnnotations;

namespace M_ID_Blog.Business.DTOs.Category
{
    public class CategoryAddDto
    {
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
        public string Name { get; set; } = null!;
        
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }
    }
}