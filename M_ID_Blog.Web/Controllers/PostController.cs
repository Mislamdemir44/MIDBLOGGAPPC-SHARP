using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using M_ID_Blog.Business.Abstract;
using M_ID_Blog.Business.DTOs.Post;
using M_ID_Blog.Entities.Concrete;
using M_ID_Blog.Web.Models;

namespace M_ID_Blog.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<AppUser> _userManager;

        public PostController(
            IPostService postService,
            ICategoryService categoryService,
            UserManager<AppUser> userManager)
        {
            _postService = postService;
            _categoryService = categoryService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Details(int id)
        {
            var post = await _postService.GetByIdAsync(id);
            if (post == null)
                return NotFound();
                
            // Increment view count
            await _postService.IncrementViewCountAsync(id);
            
            var currentUserId = _userManager.GetUserId(User);
            var viewModel = new PostDetailViewModel
            {
                Post = post,
                CurrentUserId = currentUserId
            };
            
            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllAsync();
            var viewModel = new PostCreateViewModel
            {
                Categories = categories
            };
            
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var postAddDto = new PostAddDto
                {
                    Title = model.Title,
                    Content = model.Content,
                    CategoryId = model.CategoryId,
                    Image = model.Image,
                    Video = model.Video,
                    UserId = _userManager.GetUserId(User)
                };
                
                var post = await _postService.AddAsync(postAddDto);
                return RedirectToAction(nameof(Details), new { id = post.Id });
            }
            
            model.Categories = await _categoryService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _postService.GetByIdAsync(id);
            if (post == null)
                return NotFound();
                
            var currentUser = await _userManager.GetUserAsync(User);
            if (post.User.Id != currentUser.Id && !await _userManager.IsInRoleAsync(currentUser, "Admin"))
                return Forbid();
                
            var categories = await _categoryService.GetAllAsync();
            var viewModel = new PostEditViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CategoryId = post.Category.Id,
                Categories = categories,
                ExistingImagePath = post.ImagePath,
                ExistingVideoPath = post.VideoPath
            };
            
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PostEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var post = await _postService.GetByIdAsync(model.Id);
                if (post == null)
                    return NotFound();
                    
                var currentUser = await _userManager.GetUserAsync(User);
                if (post.User.Id != currentUser.Id && !await _userManager.IsInRoleAsync(currentUser, "Admin"))
                    return Forbid();
                    
                var postUpdateDto = new PostUpdateDto
                {
                    Id = model.Id,
                    Title = model.Title,
                    Content = model.Content,
                    CategoryId = model.CategoryId,
                    Image = model.Image,
                    Video = model.Video,
                    KeepExistingImage = model.KeepExistingImage,
                    KeepExistingVideo = model.KeepExistingVideo,
                    UserId = _userManager.GetUserId(User),
                    IsActive = true
                };
                
                await _postService.UpdateAsync(postUpdateDto);
                return RedirectToAction(nameof(Details), new { id = model.Id });
            }
            
            model.Categories = await _categoryService.GetAllAsync();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _postService.GetByIdAsync(id);
            if (post == null)
                return NotFound();
                
            var currentUser = await _userManager.GetUserAsync(User);
            if (post.User.Id != currentUser.Id && !await _userManager.IsInRoleAsync(currentUser, "Admin"))
                return Forbid();
                
            await _postService.DeleteAsync(id);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyPosts()
        {
            var userId = _userManager.GetUserId(User);
            var posts = await _postService.GetAllByUserIdAsync(userId);
            
            return View(posts);
        }
    }
}