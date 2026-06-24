using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace M_ID_Blog.Business.DTOs.Post
{
    public class PostAddDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; } = null!;
        
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; } = null!;
        
        public IFormFile? Image { get; set; }
        
        public IFormFile? Video { get; set; }
        
        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }
        
        public string UserId { get; set; } = null!;
    }
}