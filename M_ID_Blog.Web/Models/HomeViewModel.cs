using M_ID_Blog.Business.DTOs.Category;
using M_ID_Blog.Business.DTOs.Post;

namespace M_ID_Blog.Web.Models
{
    public class HomeViewModel
    {
        public IList<PostListDto> Posts { get; set; } = new List<PostListDto>();
        public IList<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        public int? SelectedCategoryId { get; set; }
        public string? SearchQuery { get; set; }
    }
}