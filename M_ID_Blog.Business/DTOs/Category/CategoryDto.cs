namespace M_ID_Blog.Business.DTOs.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public int PostCount { get; set; }
    }
}