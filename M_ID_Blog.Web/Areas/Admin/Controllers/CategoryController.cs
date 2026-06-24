using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using M_ID_Blog.Business.Abstract;
using M_ID_Blog.Business.DTOs.Category;

namespace M_ID_Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Manager")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryAddDto model)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.AddAsync(model);
                return RedirectToAction(nameof(Index));
            }
            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();
                
            var model = new CategoryUpdateDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                IsActive = category.IsActive
            };
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}