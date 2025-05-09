using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public BlogPostRepository( ApplicationDBContext dBContext) 
        {
            this._dbContext = dBContext;
        }
        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await _dbContext.BlogPosts.AddAsync(blogPost);
            await _dbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteBlogPostAsync(Guid id)
        {
            var response = await _dbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
            if (response == null) return null;
            _dbContext.BlogPosts.Remove(response);
            await _dbContext.SaveChangesAsync();
            return response;

        }

        public async Task<BlogPost> GetBlogPostById(Guid id)
        {
            return await _dbContext.BlogPosts.Include(x=>x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetBlogPostByUrlHandle(string urlHandle)
        {
            return await _dbContext.BlogPosts.Include(x=>x.Categories).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<IEnumerable<BlogPost>> GetBloPostsAsync()
        {
            return await _dbContext.BlogPosts.Include(x=>x.Categories).ToListAsync();
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPosts= await _dbContext.BlogPosts.Include(x=>x.Categories).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
            if(existingBlogPosts==null)
            {
                return null;
            }
            _dbContext.Entry(existingBlogPosts).CurrentValues.SetValues(blogPost);

            existingBlogPosts.Categories = blogPost.Categories;
            await _dbContext.SaveChangesAsync();
            return blogPost;
        }
    }
}
