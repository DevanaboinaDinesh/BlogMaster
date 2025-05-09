using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IblogImage iblogImage;

        public ImagesController(IblogImage iblogImage)
        {
            this.iblogImage = iblogImage;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            var images = await iblogImage.GetAll();
            var response = new List<BlogImageDto>();
            foreach (var image in images)
            {
                response.Add(new BlogImageDto
                {
                    Id = image.Id,
                    Title = image.Title,
                    DateCreated = image.DateCreated,
                    Url = image.Url,
                    FileExtension = image.FileExtension,
                    FileName = image.FileName
                });
            }
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file, string fileName="new Coder", string title= "let's code")
        {
            ValidateFile(file);
            if (ModelState.IsValid)
            {
                var blogImage = new BlogImage
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName,
                    Title = title,
                    DateCreated=DateTime.Now,
                };
                var response=await iblogImage.UploadImage(file, blogImage);
                var responseDto = new BlogImageDto
                {
                    Id = response.Id,
                    Title = response.Title,
                    DateCreated = response.DateCreated,
                    Url = response.Url,
                    FileExtension= response.FileExtension,
                    FileName= response.FileName
                };
                return Ok(response);
            }            
            return BadRequest();
        }

        private void ValidateFile(IFormFile file)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };
            if (!(allowedExtension.Contains(Path.GetExtension(file.FileName).ToLower())))
            {
                ModelState.AddModelError("file", "Unsupported file format");
            }
            if (file.Length > 10485760)
            {
                ModelState.AddModelError("File", "File size cannot be more than 10MB");
            }
        }
    }
}
