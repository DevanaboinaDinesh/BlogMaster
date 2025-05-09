using Azure.Core;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Implementation;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ICategoryRepository categoryRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            _blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
        }
        [HttpPost]
        [Authorize(Roles = "WRITER")]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto request)
        {
            var blogpost = new BlogPost()
            {
                Title = request.Title,
                Author = request.Author,
                PublishedDate = request.PublishedDate,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                ShortDescription = request.ShortDescription,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()
            };
            foreach (var item in request.Categories)
            {
                var existingCategory = await categoryRepository.GetCategoryById(item);
                if (existingCategory != null)
                {
                    blogpost.Categories.Add(existingCategory);
                }
            }
            var response = await _blogPostRepository.CreateAsync(blogpost);
            var dto = new BlogPostDto()
            {
                Title = request.Title,
                Author = request.Author,
                PublishedDate = request.PublishedDate,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                ShortDescription = request.ShortDescription,
                UrlHandle = request.UrlHandle,
                Categories = response.Categories.Select(x => new CategoryDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()

            };
            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetBlogPosts()
        {
            var blogposts = await _blogPostRepository.GetBloPostsAsync();

            var response = new List<BlogPostDto>();

            foreach (var blogpost in blogposts)
            {
                response.Add(new BlogPostDto()
                {
                    Id = blogpost.Id,
                    Title = blogpost.Title,
                    Author = blogpost.Author,
                    PublishedDate = blogpost.PublishedDate,
                    Content = blogpost.Content,
                    UrlHandle = blogpost.UrlHandle,
                    FeaturedImageUrl = blogpost.FeaturedImageUrl,
                    IsVisible = blogpost.IsVisible,
                    ShortDescription = blogpost.ShortDescription,
                    Categories = blogpost.Categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle
                    }).ToList()
                });
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {
            var blogPost = await _blogPostRepository.GetBlogPostById(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            var response = new BlogPostDto()
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                UrlHandle = blogPost.UrlHandle,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
                PublishedDate = blogPost.PublishedDate,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("{urlHandle}")]
        public async Task<IActionResult> GetBlogPostByURL([FromRoute] string urlHandle)
        {
            var blogPost = await _blogPostRepository.GetBlogPostByUrlHandle(urlHandle);
            if(blogPost == null)
            {
                return NotFound();
            }
            var response = new BlogPostDto()
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                UrlHandle = blogPost.UrlHandle,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
                PublishedDate = blogPost.PublishedDate,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "WRITER")]
        public async Task<IActionResult> UpdateBlogPost([FromRoute] Guid id, [FromBody] UpdateBlogPostRequestDto request)
        {
            var blogPost=new BlogPost()
            {   
                Id=id,
                Title = request.Title,
                Author = request.Author,
                PublishedDate = request.PublishedDate,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                ShortDescription = request.ShortDescription,
                UrlHandle = request.UrlHandle,
                Categories=new List<Category>()

            };
            foreach( var item in request.Categories)
            {
                var existsingCategory=await categoryRepository.GetCategoryById(item);
                if(existsingCategory!=null)
                {
                    blogPost.Categories.Add(existsingCategory);
                }
            }

            var response=await _blogPostRepository.UpdateAsync(blogPost);

            var responseDto = new BlogPostDto()
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                UrlHandle = blogPost.UrlHandle,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
                PublishedDate = blogPost.PublishedDate,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };
            

            return Ok(responseDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "WRITER")]
        public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid id)
        {
            var blogPost=await _blogPostRepository.DeleteBlogPostAsync(id);
            if (blogPost == null) return NotFound("No BlogPost with the Given Id");
            var responseDto = new BlogPostDto()
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                UrlHandle = blogPost.UrlHandle,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
                PublishedDate = blogPost.PublishedDate,               
            };
            return Ok(responseDto);
        }
         
    }
}
