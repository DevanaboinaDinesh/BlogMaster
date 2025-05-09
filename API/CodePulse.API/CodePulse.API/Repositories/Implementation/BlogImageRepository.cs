using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class BlogImageRepository : IblogImage
    {
        private readonly ApplicationDBContext dBContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public BlogImageRepository(ApplicationDBContext dBContext, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.dBContext = dBContext;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<BlogImage>> GetAll()
        {
            return await dBContext.BlogImages.ToListAsync();
        }

        public async Task<BlogImage> UploadImage(IFormFile file, BlogImage blogImage)
        {
            // Upload the the image to api/image folder;
            var localPath=Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{blogImage.FileName}{blogImage.FileExtension}");
            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);

            // Update the table with the new image
            // https://codeuplse.com/images/filename.jpg
            var httpRequest = httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{blogImage.FileName}{blogImage.FileExtension}";
            blogImage.Url = urlPath;
            await dBContext.BlogImages.AddAsync(blogImage);
            await dBContext.SaveChangesAsync();
            return blogImage;
        }
    }
}
