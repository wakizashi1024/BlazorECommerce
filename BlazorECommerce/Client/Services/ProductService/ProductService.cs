
using System.Collections.ObjectModel;

namespace BlazorECommerce.Client.Services.ProductService;

public class ProductService : IProductService
{
    private readonly HttpClient _http;

    public event Action ProductsChanged;

    public ICollection<Product> Products { get; set; }
    public string Message { get; set; } = "Loading products...";

    public ProductService(HttpClient http)
    {
        _http = http;
    }

    public async Task GetProducts(string? categoryUrl = null)
    {
        BeforeRequestProducts();

        var result = categoryUrl is null 
            ? await _http.GetFromJsonAsync<ServiceResponse<ICollection<Product>>>("api/Product/featured")
            : await _http.GetFromJsonAsync<ServiceResponse<ICollection<Product>>>($"api/Product/category/{categoryUrl}");

        if (result is not null && result.Data is not null)
        {
            Products = result.Data;
        }

        AfterRequestProducts();
    }

    public async Task<ServiceResponse<Product>> GetProduct(int productId)
    {
        var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/Product/{productId}");

        return result;
    }

    public async Task SearchProducts(string searchText)
    {
        BeforeRequestProducts();

        var result = await _http.GetFromJsonAsync<ServiceResponse<ICollection<Product>>>($"api/product/search/{searchText}");

        if (result is not null)
        {
            Products = result.Data;
        }

        AfterRequestProducts();
    }

    public async Task<IEnumerable<string>> GetProductSearchSuggestion(string searchText)
    {
        var result = await _http.GetFromJsonAsync<ServiceResponse<IEnumerable<string>>>($"/api/Product/search-suggestions/{searchText}");

        return result.Data;
    }

    private void BeforeRequestProducts()
    {
        Products = new Collection<Product>();
        Message = "Loading products...";
        ProductsChanged?.Invoke();
    }

    private void AfterRequestProducts()
    {
        if (Products is not null && Products.Count == 0)
        {
            Message = "No products found.";
        }

        ProductsChanged?.Invoke();
    }
}
