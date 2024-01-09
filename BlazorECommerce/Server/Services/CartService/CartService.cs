
namespace BlazorECommerce.Server.Services.CartService;

public class CartService : ICartService
{
    private readonly DataContext _context;

    public CartService(DataContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<ICollection<CartProductResponseDto>>> GetCartProdcuts(IEnumerable<CartItem> cartItems)
    {
        var result = new ServiceResponse<ICollection<CartProductResponseDto>>
        {
            Data = new List<CartProductResponseDto>(),
        };

        foreach (var cartItem in cartItems)
        {
            var product = await _context.Products
                            .Where(p => p.Id == cartItem.ProductId)
                            .FirstOrDefaultAsync();
            if (product == null)
            {
                continue;
            }

            var productVariant = await _context.ProductVariants
                .Where(v => v.ProductId == cartItem.ProductId
                       && v.ProductTypeId == cartItem.ProductTypeId)
                .Include(v => v.ProductType)
                .FirstOrDefaultAsync();

            if (productVariant == null)
            {
                continue;
            }

            var cartProduct = new CartProductResponseDto
            {
                ProductId = product.Id,
                Title = product.Title,
                ImageUrl = product.ImageUrl,
                Price = productVariant.Price,
                ProductType = productVariant.ProductType.Name,
                ProductTypeId = productVariant.ProductTypeId,
                Quantity = cartItem.Quantity,
            };

            result.Data.Add(cartProduct);
        }

        return result;
    }
}
