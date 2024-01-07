namespace BlazorECommerce.Client.Services.ProductService;

public interface IProductService
{
    event Action ProductsChanged;
    ICollection<Product> Products { get; set; }
    string Message { get; set; }
    Task GetProducts(string? categoryUrl = null);
    Task<ServiceResponse<Product>> GetProduct(int productId);
    Task SearchProducts(string searchText);
    Task<IEnumerable<string>> GetProductSearchSuggestion(string searchText);
}
