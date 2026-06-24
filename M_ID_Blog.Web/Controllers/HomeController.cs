using Microsoft.AspNetCore.Mvc;
using M_ID_Blog.Business.Abstract;
using M_ID_Blog.Web.Models;

namespace M_ID_Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;

        public HomeController(IPostService postService, ICategoryService categoryService)
        {
            _postService = postService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int? categoryId = null)
        {
            var viewModel = new HomeViewModel
            {
                Categories = await _categoryService.GetAllAsync()
            };
            
            if (categoryId.HasValue)
            {
                viewModel.Posts = await _postService.GetAllByCategoryIdAsync(categoryId.Value);
                viewModel.SelectedCategoryId = categoryId.Value;
            }
            else
            {
                viewModel.Posts = await _postService.GetAllAsync();
            }
            
            return View(viewModel);
        }

        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return RedirectToAction(nameof(Index));
                
            var allPosts = await _postService.GetAllAsync();
            var filteredPosts = allPosts
                .Where(p => p.Title.Contains(query, StringComparison.OrdinalIgnoreCase) || 
                           p.Content.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();
                
            var viewModel = new HomeViewModel
            {
                Posts = filteredPosts,
                Categories = await _categoryService.GetAllAsync(),
                SearchQuery = query
            };
            
            return View("Index", viewModel);
        }
    }
}