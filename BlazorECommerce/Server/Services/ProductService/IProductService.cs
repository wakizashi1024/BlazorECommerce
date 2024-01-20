namespace BlazorECommerce.Server.Services.ProductService;

public interface IProductService
{
    Task<ServiceResponse<IEnumerable<Product>>> GetProductsAsync();
    Task<ServiceResponse<Product>> GetProductAsync(int productId);
    Task<ServiceResponse<IEnumerable<Product>>> GetProductsByCategoryAsync(string categoryUrl);
    Task<ServiceResponse<ProductSearchResultDto>> SearchProducts(string searchText, int page, int pageSize = 2);
    Task<ServiceResponse<IEnumerable<string>>> GetProductSearchSuggestions(string searchText);
    Task<ServiceResponse<IEnumerable<Product>>> GetFeaturedProducts();
    Task<ServiceResponse<IEnumerable<Product>>> GetAdminProducts();
    Task<ServiceResponse<Product>> CreateProduct(Product product);
    Task<ServiceResponse<Product>> UpdateProduct(Product product);
    Task<ServiceResponse<bool>> DeleteProduct(int productId);
}
