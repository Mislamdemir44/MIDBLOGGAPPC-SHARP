using M_ID_Blog.Business.DTOs.Category;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace M_ID_Blog.Web.Models
{
    public class PostEditViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; } = null!;
        
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; } = null!;
        
        public IFormFile? Image { get; set; }
        
        public IFormFile? Video { get; set; }
        
        [Display(Name = "Keep Existing Image")]
        public bool KeepExistingImage { get; set; } = true;
        
        [Display(Name = "Keep Existing Video")]
        public bool KeepExistingVideo { get; set; } = true;
        
        public string? ExistingImagePath { get; set; }
        
        public string? ExistingVideoPath { get; set; }
        
        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        
        public IList<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
    }
}