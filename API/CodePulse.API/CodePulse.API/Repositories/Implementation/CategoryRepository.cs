using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public CategoryRepository(ApplicationDBContext dBContext) 
        { 
            _dbContext = dBContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }        
        public async Task<Category?> GetCategoryById(Guid id)
        {
            var response = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            return response;                        
        }
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(string? query=null, string? sortBy = null, string? sortDirection = null, int? pageNumber = 1, int?pageSize = 100)
        {
            //var response=await _dbContext.Categories.ToListAsync(); 
            //return response;
            var categories = _dbContext.Categories.AsQueryable();

            // Filtering

            if (string.IsNullOrWhiteSpace(query) == false)
            {
                categories = categories.Where(x => x.Name.Contains(query));
            }

            // Sorting

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(string.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;

                    categories = isAsc ? categories.OrderBy(x => x.Name) : categories.OrderByDescending(x => x.Name);
                }
                if (string.Equals(sortBy, "URL", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;

                    categories = isAsc ? categories.OrderBy(x => x.UrlHandle) : categories.OrderByDescending(x => x.UrlHandle);
                }
            }

            //Pagination

            var skipResults = (pageNumber - 1) * pageSize;
            categories = categories.Skip(skipResults ?? 0).Take(pageSize ?? 100);


            return await categories.ToListAsync();
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var response=await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            if(response != null)
            {
                _dbContext.Entry(response).CurrentValues.SetValues(category);
                await _dbContext.SaveChangesAsync();
            }            
            return response;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var response=await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (response == null) return response;
            _dbContext.Categories.Remove(response);
            await _dbContext.SaveChangesAsync();
            return response;
        }

        public async Task<int> GetCount()
        {
            return await _dbContext.Categories.CountAsync();
        }
    }
}
