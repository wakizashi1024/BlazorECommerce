namespace BlazorECommerce.Server.Services.ProductTypeService;

public interface IProductTypeService
{
    Task<ServiceResponse<IEnumerable<ProductType>>> GetProductTypesAsync();
    Task<ServiceResponse<IEnumerable<ProductType>>> CreateProductTypesAsync(ProductType productType);
    Task<ServiceResponse<IEnumerable<ProductType>>> UpdateProductTypesAsync(ProductType productType);
}
