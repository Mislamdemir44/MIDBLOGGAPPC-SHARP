using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using M_ID_Blog.Business.Abstract;
using M_ID_Blog.DataAccess.Concrete.UnitOfWork;

namespace M_ID_Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Manager")]
    public class DashboardController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IPostService _postService;
        private readonly UnitOfWork _unitOfWork;

        public DashboardController(ICategoryService categoryService, IPostService postService, UnitOfWork unitOfWork)
        {
            _categoryService = categoryService;
            _postService = postService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new DashboardViewModel
            {
                CategoryCount = await _unitOfWork.Categories.CountAsync(c => c.IsActive),
                PostCount = await _unitOfWork.Posts.CountAsync(p => p.IsActive),
                CommentCount = await _unitOfWork.Comments.CountAsync(c => c.IsActive),
                UserCount = _unitOfWork._context.Users.Count(),
                LatestPosts = await _postService.GetAllAsync()
            };
            
            viewModel.LatestPosts = viewModel.LatestPosts.Take(5).ToList();
            
            return View(viewModel);
        }
    }
}