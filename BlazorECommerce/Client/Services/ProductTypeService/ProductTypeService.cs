
namespace BlazorECommerce.Client.Services.ProductTypeService;

public class ProductTypeService : IProductTypeService
{
    private readonly HttpClient _http;

    public ICollection<ProductType> ProductTypes { get; set; } = new List<ProductType>();

    public event Action OnChange;

    public ProductTypeService(HttpClient http)
    {
        _http = http;
    }

    public async Task GetProductTypes()
    {
        var result = await _http.GetFromJsonAsync<ServiceResponse<List<ProductType>>>("api/ProductType");

        if (result is not null && result.Data is not null)
        {
            ProductTypes = result.Data;
        }
    }

    public async Task CreateProductType(ProductType productType)
    {
        var response = await _http.PostAsJsonAsync("api/ProductType", productType);
        ProductTypes = (await response.Content
            .ReadFromJsonAsync<ServiceResponse<ICollection<ProductType>>>())
            .Data;

        OnChange?.Invoke();
    }

    public async Task UpdateProductType(ProductType productType)
    {
        var response = await _http.PutAsJsonAsync("api/ProductType", productType);
        ProductTypes = (await response.Content
            .ReadFromJsonAsync<ServiceResponse<ICollection<ProductType>>>())
            .Data;

        OnChange?.Invoke();
    }

    public ProductType GenNewProductType()
    {
        var newProductType = new ProductType
        {
            IsNew = true,
            Editing = true,
        };
        ProductTypes.Add(newProductType);

        OnChange?.Invoke();

        return newProductType;
    }
}
