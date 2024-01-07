namespace BlazorECommerce.Server.Services.ProductService;

public interface IProductService
{
    Task<ServiceResponse<IEnumerable<Product>>> GetProductsAsync();
    Task<ServiceResponse<Product>> GetProductAsync(int productId);
    Task<ServiceResponse<IEnumerable<Product>>> GetProductsByCategoryAsync(string categoryUrl);
    Task<ServiceResponse<IEnumerable<Product>>> SearchProducts(string searchText);
    Task<ServiceResponse<IEnumerable<string>>> GetProductSearchSuggestions(string searchText);
    Task<ServiceResponse<IEnumerable<Product>>> GetFeaturedProducts();
}
