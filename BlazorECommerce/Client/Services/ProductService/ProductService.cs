
namespace BlazorECommerce.Client.Services.ProductService;

public class ProductService : IProductService
{
    private readonly HttpClient _http;

    //public event Action ProductsChanged;

    public ICollection<Product> Products { get; set; }

    public ProductService(HttpClient http)
    {
        _http = http;
    }

    public async Task GetProducts(string? categoryUrl = null)
    {
        var result = categoryUrl is null 
            ? await _http.GetFromJsonAsync<ServiceResponse<ICollection<Product>>>("api/Product")
            : await _http.GetFromJsonAsync<ServiceResponse<ICollection<Product>>>($"api/Product/category/{categoryUrl}");

        if (result is not null && result.Data is not null)
        {
            Products = result.Data;
        }

        // ProductsChanged.Invoke();
    }

    public async Task<ServiceResponse<Product>> GetProduct(int productId)
    {
        var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/Product/{productId}");

        return result;
    }
}
