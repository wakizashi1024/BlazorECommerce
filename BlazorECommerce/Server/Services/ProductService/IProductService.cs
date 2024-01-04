namespace BlazorECommerce.Server.Services.ProductService;

public interface IProductService
{
    Task<ServiceResponse<IEnumerable<Product>>> GetProductsAsync();
    Task<ServiceResponse<Product>> GetProductAsync(int productId);
    Task<ServiceResponse<IEnumerable<Product>>> GetProductsByCategoryAsync(string categoryUrl);
}
