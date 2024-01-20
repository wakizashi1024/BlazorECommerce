namespace BlazorECommerce.Client.Services.CategoryService;

public interface ICategoryService
{
    event Action OnChange;
    ICollection<Category> Categories { get; set; }
    ICollection<Category> AdminCategories { get; set; }
    Task GetCategories();
    Task GetAdminCategories();
    Task CreateCategory(Category category);
    Task UpdateCategory(Category category);
    Task DeleteCategory(int categoryId);
    Category GenNewCategory();
}
