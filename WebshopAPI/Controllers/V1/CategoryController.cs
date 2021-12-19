using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopServices.Features.Interfaces;
using WebshopShared.ResponseModels;
using WebshopShared.RequestModels;
using System;

namespace WebshopAPI.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        public readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<ActionResult<CategoryResponse>> CreateCategory(CategoryRequest categoryRequest)
        {
            var response = await _categoryService.CreateCategory(categoryRequest);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryResponse>>> GetAllCategories()
        {
            var response = await _categoryService.GetAllCategories();
            return Response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{categoryId}")]
        public async Task<ActionResult<CategoryResponse>> GetCategoryById(Guid categoryId)
        {
            var response = await _categoryService.GetCategoryById(categoryId);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("name/{categoryName}")]
        public async Task<ActionResult<CategoryResponse>> GetCategoryByName(string categoryName)
        {
            var response = await _categoryService.GetCategoryByName(categoryName);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{categoryId}")]
        public async Task<ActionResult<Guid>> DeleteCategory([FromRoute] Guid categoryID )
        {
            var response = await _categoryService.DeleteCategory(categoryID);
            return response == Guid.Empty ? BadRequest(response) : Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<CategoryResponse>> UpdateCategory(CategoryRequest categoryRequest)
        {
            var response = await _categoryService.UpdateCategory(categoryRequest);
            return response != null ? Ok(response) : BadRequest(response);
        }


    }
}
