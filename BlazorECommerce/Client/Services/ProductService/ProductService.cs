namespace BlazorECommerce.Client.Services.ProductService;

public class ProductService : IProductService
{
    private readonly HttpClient _http;

    public ICollection<Product> Products { get; set; } = new List<Product>();

    public ProductService(HttpClient http)
    {
        _http = http;
    }

    public async Task GetProducts()
    {
        var result = await _http.GetFromJsonAsync<ServiceResponse<ICollection<Product>>>("api/Product");

        if (result is not null && result.Data is not null)
        {
            Products = result.Data;
        }
    }
}
