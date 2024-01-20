namespace BlazorECommerce.Server.Services.ProductTypeService;

public class ProductTypeService : IProductTypeService
{
    private readonly DataContext _context;

    public ProductTypeService(DataContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<IEnumerable<ProductType>>> CreateProductTypesAsync(ProductType productType)
    {
        _context.ProductTypes.Add(productType);
        await _context.SaveChangesAsync();

        return await GetProductTypesAsync();
    }

    public async Task<ServiceResponse<IEnumerable<ProductType>>> GetProductTypesAsync()
    {
        var productTypes = await _context.ProductTypes.ToListAsync();

        return new ServiceResponse<IEnumerable<ProductType>>
        {
            Data = productTypes,
        };
    }

    public async Task<ServiceResponse<IEnumerable<ProductType>>> UpdateProductTypesAsync(ProductType productType)
    {
        var dbProductType = await _context.ProductTypes.FirstOrDefaultAsync(pt => pt.Id == productType.Id);
        if (dbProductType is null)
        {
            return new ServiceResponse<IEnumerable<ProductType>>
            {
                Success = false,
                Message = "Product type not found.",
            };
        }

        dbProductType.Name = productType.Name;
        await _context.SaveChangesAsync();

        return await GetProductTypesAsync();
    }
}
