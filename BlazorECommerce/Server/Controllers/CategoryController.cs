using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorECommerce.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        var result = await _categoryService.GetCategoriesAsync();

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("admin")]
    public async Task<ActionResult<IEnumerable<Category>>> GetAdminCategories()
    {
        var result = await _categoryService.GetAdminCategoriesAsync();

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("admin")]
    public async Task<ActionResult<IEnumerable<Category>>> CreateCategory(Category category)
    {
        var result = await _categoryService.CreateCategoryAsync(category);

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("admin")]
    public async Task<ActionResult<IEnumerable<Category>>> UpdateCategory(Category category)
    {
        var result = await _categoryService.UpdateCategoryAsync(category);

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("admin/{categoryId}")]
    public async Task<ActionResult<IEnumerable<Category>>> DeleteCategory(int categoryId)
    {
        var result = await _categoryService.DeleteCategoryAsync(categoryId);

        return Ok(result);
    }
}
