namespace BlazorECommerce.Server.Services.CategoryService;

public interface ICategoryService
{
    Task<ServiceResponse<IEnumerable<Category>>> GetCategoriesAsync();
}
