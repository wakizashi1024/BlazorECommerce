namespace BlazorECommerce.Server.Services.CategoryService;

public interface ICategoryService
{
    Task<ServiceResponse<IEnumerable<Category>>> GetCategoriesAsync();

    // Admin methods
    Task<ServiceResponse<IEnumerable<Category>>> GetAdminCategoriesAsync();
    Task<ServiceResponse<IEnumerable<Category>>> CreateCategoryAsync(Category category);
    Task<ServiceResponse<IEnumerable<Category>>> UpdateCategoryAsync(Category category);
    Task<ServiceResponse<IEnumerable<Category>>> DeleteCategoryAsync(int id);
}
