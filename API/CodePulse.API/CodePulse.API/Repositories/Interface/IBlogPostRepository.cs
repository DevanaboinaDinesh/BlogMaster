using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetBloPostsAsync();
        Task<BlogPost?> GetBlogPostById(Guid id);
        Task<BlogPost?> GetBlogPostByUrlHandle(string urlHandle);
        Task<BlogPost?> UpdateAsync(BlogPost blogPost);        
        Task<BlogPost?> DeleteBlogPostAsync(Guid id);
    }
}
