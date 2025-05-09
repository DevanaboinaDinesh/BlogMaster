using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface IblogImage
    {
        Task<BlogImage> UploadImage(IFormFile file, BlogImage blogImage);
        Task<IEnumerable<BlogImage>> GetAll();
    }
}
