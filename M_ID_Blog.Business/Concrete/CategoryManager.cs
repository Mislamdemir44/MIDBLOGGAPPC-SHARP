using AutoMapper;
using M_ID_Blog.Business.Abstract;
using M_ID_Blog.Business.DTOs.Category;
using M_ID_Blog.DataAccess.Concrete.UnitOfWork;
using M_ID_Blog.Entities.Concrete;

namespace M_ID_Blog.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryManager(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<CategoryDto>> GetAllAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => c.IsActive);
            var categoryDtos = _mapper.Map<IList<CategoryDto>>(categories);
            
            foreach (var categoryDto in categoryDtos)
            {
                categoryDto.PostCount = await _unitOfWork.Posts.CountAsync(p => p.CategoryId == categoryDto.Id && p.IsActive);
            }
            
            return categoryDtos;
        }

        public async Task<CategoryDto?> GetByIdAsync(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId && c.IsActive);
            if (category == null)
                return null;
                
            var categoryDto = _mapper.Map<CategoryDto>(category);
            categoryDto.PostCount = await _unitOfWork.Posts.CountAsync(p => p.CategoryId == categoryId && p.IsActive);
            
            return categoryDto;
        }

        public async Task<CategoryDto> AddAsync(CategoryAddDto categoryAddDto)
        {
            var category = _mapper.Map<Category>(categoryAddDto);
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveAsync();
            
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.Id);
            if (category != null)
            {
                category = _mapper.Map(categoryUpdateDto, category);
                await _unitOfWork.Categories.UpdateAsync(category);
                await _unitOfWork.SaveAsync();
            }
            
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task DeleteAsync(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                category.IsActive = false;
                await _unitOfWork.Categories.UpdateAsync(category);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}