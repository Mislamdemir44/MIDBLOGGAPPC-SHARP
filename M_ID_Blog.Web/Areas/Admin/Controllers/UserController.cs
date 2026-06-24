using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using M_ID_Blog.Entities.Concrete;
using M_ID_Blog.Web.Areas.Admin.Models;

namespace M_ID_Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var userViewModels = new List<UserViewModel>();
            
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                
                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FullName = user.FullName,
                    ProfileImagePath = user.ProfileImagePath,
                    CreatedDate = user.CreatedDate,
                    IsActive = user.IsActive,
                    Roles = roles.ToList()
                });
            }
            
            return View(userViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
                
            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.ToList();
            
            var model = new UserEditViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                IsActive = user.IsActive,
                UserRoles = userRoles.ToList(),
                AllRoles = allRoles.Select(r => r.Name).ToList()
            };
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user == null)
                    return NotFound();
                    
                user.FullName = model.FullName;
                user.IsActive = model.IsActive;
                
                await _userManager.UpdateAsync(user);
                
                // Update roles
                var userRoles = await _userManager.GetRolesAsync(user);
                
                // Remove roles that are not selected
                foreach (var role in userRoles)
                {
                    if (!model.SelectedRoles.Contains(role))
                    {
                        await _userManager.RemoveFromRoleAsync(user, role);
                    }
                }
                
                // Add newly selected roles
                foreach (var role in model.SelectedRoles)
                {
                    if (!userRoles.Contains(role))
                    {
                        await _userManager.AddToRoleAsync(user, role);
                    }
                }
                
                return RedirectToAction(nameof(Index));
            }
            
            return View(model);
        }
    }
}