using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace M_ID_Blog.Business.DTOs.User
{
    public class UserUpdateDto
    {
        [Required]
        public string Id { get; set; } = null!;
        
        [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters")]
        public string? FullName { get; set; }
        
        [StringLength(500, ErrorMessage = "Biography cannot exceed 500 characters")]
        public string? Biography { get; set; }
        
        public IFormFile? ProfileImage { get; set; }
        
        public bool KeepExistingImage { get; set; } = true;
    }
}