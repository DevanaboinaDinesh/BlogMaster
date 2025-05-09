using System.Formats.Asn1;
using CodePulse.API.Data;
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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _icategoryservice;
        public CategoriesController(ICategoryRepository service)
        {
            _icategoryservice = service;
        }

        [HttpPost]
        //[Authorize(Roles = "WRITER")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto categoryDto)
        {
            // map dto to domain model
            var category = new Category()
            {
                Name = categoryDto.Name,
                UrlHandle = categoryDto.UrlHandle,
            };
            await _icategoryservice.CreateAsync(category);

            // domain model to DTo
            var response = new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };
            return Ok(response);
        }

        //http://localhost:5086/api/Categories
        // with Query //http://localhost:5086/api/Categories?query&sortBy=name&sortDirection=asc
        [HttpGet]        
        public async Task<IActionResult> GetAllCategories([FromQuery(Name = "query")] string? query,
            [FromQuery] string? sortBy,
            [FromQuery] string? sortDirection,
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize)
        {
            var categories= await _icategoryservice.GetAllCategoriesAsync(query,sortBy,sortDirection,pageNumber,pageSize);
            //Map domain model to dto
            var response = new List<CategoryDto>();
            foreach (var category in categories)
            {
                var dto = new CategoryDto()
                {
                    Id=category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle,
                };
                response.Add(dto);
            }
            return Ok(response);
        }
        ////http://localhost:5086/api/Categories/count
        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> GetCategoriesCount()
        {
           var count = await _icategoryservice.GetCount();
            return Ok(count);
        }

        // GET: http://localhost:5086/api/Categories/{id}
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var response=await _icategoryservice.GetCategoryById(id);
            if (response == null) return NotFound("No Record found with the Given Id");
            var dto = new CategoryDto()
            {
                Id = response.Id,
                Name = response.Name,
                UrlHandle = response.UrlHandle,
            };
            return Ok(dto);
        }

        //PUT http://localhost:5086/api/Categories/{id}
        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "WRITER")]
        public async Task<IActionResult> UpdateCategoryById([FromRoute] Guid id,[FromBody] UpdateCategoryRequestDto request)
        {
            var model = new Category()
            {
                Id = id,
                Name=request.Name,
                UrlHandle=request.UrlHandle
            };
            var response=await _icategoryservice.UpdateAsync(model);
            if (response == null) return NotFound("Update Failed");
            var dto = new CategoryDto()
            {
                Id=response.Id,
                Name=response.Name,
                UrlHandle=response.UrlHandle
            };
            return Ok(dto);
        }

        //DELETE http://localhost:5086/api/Categories/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "WRITER")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var response=await _icategoryservice.DeleteAsync(id);
            if (response == null) return NotFound("No item found to Delete");
            var dto = new CategoryDto()
            {
                Id =id,
                Name=response.Name,
                UrlHandle=response.UrlHandle
            };
            return Ok(dto);

        }
    }
}
