
namespace BlazorECommerce.Server.Services.ProductService;

public class ProductService : IProductService
{
    private readonly DataContext _context;

    public ProductService(DataContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<IEnumerable<Product>>> GetProductsAsync()
    {
        var response = new ServiceResponse<IEnumerable<Product>>()
        {
            Data = await _context.Products.ToListAsync(),
        };

        return response;
    }
}
