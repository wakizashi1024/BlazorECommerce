namespace BlazorECommerce.Server.Services.ProductService;

public interface IProductService
{
    Task<ServiceResponse<IEnumerable<Product>>> GetProductsAsync();
}
