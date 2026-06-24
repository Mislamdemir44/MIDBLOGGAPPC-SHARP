using M_ID_Blog.Entities.Concrete;
using M_ID_Blog.Business.DTOs.Category;

namespace M_ID_Blog.Business.Abstract
{
    public interface ICategoryService
    {
        Task<IList<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(int categoryId);
        Task<CategoryDto> AddAsync(CategoryAddDto categoryAddDto);
        Task<CategoryDto> UpdateAsync(CategoryUpdateDto categoryUpdateDto);
        Task DeleteAsync(int categoryId);
    }
}