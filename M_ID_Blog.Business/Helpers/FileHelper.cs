using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace M_ID_Blog.Business.Helpers
{
    public static class FileHelper
    {
        public static async Task<string> SaveFileAsync(IFormFile file, string folderName, IWebHostEnvironment env)
        {
            if (file == null || file.Length == 0)
                return string.Empty;
                
            var uploadsFolderPath = Path.Combine(env.WebRootPath, "uploads", folderName);
            
            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);
                
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);
            
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            
            return $"/uploads/{folderName}/{fileName}";
        }
        
        public static void DeleteFile(string filePath, IWebHostEnvironment env)
        {
            if (string.IsNullOrEmpty(filePath))
                return;
                
            var fullPath = Path.Combine(env.WebRootPath, filePath.TrimStart('/'));
            
            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }
}