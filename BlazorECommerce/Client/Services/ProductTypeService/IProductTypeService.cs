namespace BlazorECommerce.Client.Services.ProductTypeService;

public interface IProductTypeService
{
    event Action OnChange;

    public ICollection<ProductType> ProductTypes { get; set; }
    Task GetProductTypes();
    Task CreateProductType(ProductType productType);
    Task UpdateProductType(ProductType productType);
    ProductType GenNewProductType();
}
