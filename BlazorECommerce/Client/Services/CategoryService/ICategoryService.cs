namespace BlazorECommerce.Client.Services.CategoryService
{
    public interface ICategoryService
    {
        ICollection<Category> Categories { get; set; }
        Task GetCategories();
    }
}
