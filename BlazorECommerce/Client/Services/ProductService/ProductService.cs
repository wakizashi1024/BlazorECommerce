
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BlazorECommerce.Client.Services.ProductService;

public class ProductService : IProductService
{
    private readonly HttpClient _http;

    public event Action ProductsChanged;
    public int CurrentPage { get; set; } = 1;
    public int PageCount { get; set; } = 0;
    public string LastSearchText { get; set; }
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
        CurrentPage = 1;
        PageCount = 0;

        if (Products.Count == 0)
        {
            Message = "No prodcut found";
        }

        AfterRequestProducts();
    }

    public async Task<ServiceResponse<Product>> GetProduct(int productId)
    {
        var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/Product/{productId}");

        return result;
    }

    public async Task SearchProducts(string searchText, int page)
    {
        LastSearchText = searchText;
        BeforeRequestProducts();

        var result = await _http.GetFromJsonAsync<ServiceResponse<ProductSearchResultDto>>($"api/product/search/{searchText}/{page}");

        if (result is not null)
        {
            Products = (ICollection<Product>)result.Data.Products;
            PageCount = result.Data.Pages;
            CurrentPage = result.Data.CurrentPage;
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
