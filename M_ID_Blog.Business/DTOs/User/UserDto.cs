namespace M_ID_Blog.Business.DTOs.User
{
    public class UserDto
    {
        public string Id { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? FullName { get; set; }
        public string? Biography { get; set; }
        public string? ProfileImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}