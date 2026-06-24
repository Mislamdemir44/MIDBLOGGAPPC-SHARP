using M_ID_Blog.Business.Abstract;
using M_ID_Blog.Business.Concrete;
using M_ID_Blog.Business.Mapping;
using M_ID_Blog.Core.DataAccess.UnitOfWork;
using M_ID_Blog.DataAccess.Concrete.EntityFramework;
using M_ID_Blog.DataAccess.Concrete.UnitOfWork;
using M_ID_Blog.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Add DbContext
builder.Services.AddDbContext<MidBlogContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity
builder.Services.AddIdentity<AppUser, IdentityRole>(options => 
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
})
.AddEntityFrameworkStores<MidBlogContext>()
.AddDefaultTokenProviders();

// Configure cookie settings
builder.Services.ConfigureApplicationCookie(options => 
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(14);
    options.SlidingExpiration = true;
});

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add Unit of Work
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add Services
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IPostService, PostManager>();
builder.Services.AddScoped<ICommentService, CommentManager>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Seed Roles
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Manager", "User" };
    
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

// Seed Admin User
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    string email = "admin@mid.com";
    string password = "Admin123!";
    
    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new AppUser
        {
            UserName = email,
            Email = email,
            FullName = "Admin User",
            EmailConfirmed = true
        };
        
        await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Admin");
    }
}

app.Run();