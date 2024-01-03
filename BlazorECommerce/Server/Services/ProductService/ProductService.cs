
namespace BlazorECommerce.Server.Services.ProductService;

public class ProductService : IProductService
{
    private readonly DataContext _context;

    public ProductService(DataContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
    {
        var response = new ServiceResponse<Product>();
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        if (product == null) {
            response.Success = false;
            response.Message = "Product does not exist.";
            
        }
        else
        {
            response.Data = product;
        }

        return response;
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
