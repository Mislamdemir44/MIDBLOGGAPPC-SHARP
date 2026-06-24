using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using M_ID_Blog.Business.Abstract;
using M_ID_Blog.Business.DTOs.Comment;
using M_ID_Blog.Entities.Concrete;

namespace M_ID_Blog.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<AppUser> _userManager;

        public CommentController(ICommentService commentService, UserManager<AppUser> userManager)
        {
            _commentService = commentService;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CommentAddDto model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = _userManager.GetUserId(User);
                await _commentService.AddAsync(model);
                
                return RedirectToAction("Details", "Post", new { id = model.PostId });
            }
            
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reply(CommentReplyDto model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = _userManager.GetUserId(User);
                await _commentService.AddReplyAsync(model);
                
                return RedirectToAction("Details", "Post", new { id = model.PostId });
            }
            
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CommentUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                var comment = await _commentService.GetByIdAsync(model.Id);
                if (comment == null)
                    return NotFound();
                    
                var currentUser = await _userManager.GetUserAsync(User);
                if (comment.User.Id != currentUser.Id && !await _userManager.IsInRoleAsync(currentUser, "Admin"))
                    return Forbid();
                    
                model.UserId = _userManager.GetUserId(User);
                await _commentService.UpdateAsync(model);
                
                return RedirectToAction("Details", "Post", new { id = comment.PostId });
            }
            
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int postId)
        {
            var comment = await _commentService.GetByIdAsync(id);
            if (comment == null)
                return NotFound();
                
            var currentUser = await _userManager.GetUserAsync(User);
            if (comment.User.Id != currentUser.Id && !await _userManager.IsInRoleAsync(currentUser, "Admin"))
                return Forbid();
                
            await _commentService.DeleteAsync(id);
            
            return RedirectToAction("Details", "Post", new { id = postId });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Like(int id, int postId)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _commentService.LikeCommentAsync(id, userId);
            
            return RedirectToAction("Details", "Post", new { id = postId });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Unlike(int id, int postId)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _commentService.UnlikeCommentAsync(id, userId);
            
            return RedirectToAction("Details", "Post", new { id = postId });
        }
    }
}