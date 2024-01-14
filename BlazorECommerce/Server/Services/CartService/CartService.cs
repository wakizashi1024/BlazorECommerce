using BlazorECommerce.Shared;
using System.Security.Claims;

namespace BlazorECommerce.Server.Services.CartService;

public class CartService : ICartService
{
    private readonly DataContext _context;
    private readonly IAuthService _authService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartService(DataContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
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

    // public async Task<ServiceResponse<ICollection<CartProductResponseDto>>> StoreCartItems(ICollection<CartItem> cartItems, int userId)
    public async Task<ServiceResponse<ICollection<CartProductResponseDto>>> StoreCartItems(ICollection<CartItem> cartItems)
    {
        foreach (var cartItem in cartItems)
        {
            // cartItem.UserId = userId;
            cartItem.UserId = _authService.GetUserId();
        }
        _context.CartItems.AddRange(cartItems);
        await _context.SaveChangesAsync();

        return await GetDbCartProducts();
    }

    public async Task<ServiceResponse<int>> GetCartItemsCount()
    {
        var count = await _context.CartItems
            .Where(ci => ci.UserId == _authService.GetUserId())
            .CountAsync();

        return new ServiceResponse<int> 
        {
            Data = count,
        };
    }

    public async Task<ServiceResponse<ICollection<CartProductResponseDto>>> GetDbCartProducts()
    {
        return await GetCartProdcuts(await _context.CartItems
            .Where(ci => ci.UserId == _authService.GetUserId()).ToListAsync());
    }

    public async Task<ServiceResponse<bool>> AddToCart(CartItem cartItem)
    {
        cartItem.UserId = _authService.GetUserId();

        var sameItem = await _context.CartItems
            .FirstOrDefaultAsync(ci => ci.ProductId == cartItem.ProductId
                && ci.ProductTypeId == cartItem.ProductTypeId && ci.UserId == cartItem.UserId
            );
        if (sameItem is null)
        {
            _context.CartItems.Add(cartItem);
        }
        else 
        {
            sameItem.Quantity += cartItem.Quantity;
        }

        await _context.SaveChangesAsync();

        return new ServiceResponse<bool>
        {
            Data = true
        };
    }

    public async Task<ServiceResponse<bool>> UpdateQuantity(CartItem cartItem)
    {
        var dbCartItem = await _context.CartItems
            .FirstOrDefaultAsync(ci => ci.ProductId == cartItem.ProductId
                && ci.ProductTypeId == cartItem.ProductTypeId && ci.UserId == _authService.GetUserId()
            );
        if (dbCartItem is null)
        {
            return new ServiceResponse<bool>
            {
                Success = false,
                Data = false,
                Message = "Cart item does not exist.",
            };
        }
        else
        {
            dbCartItem.Quantity = cartItem.Quantity;
        }

        await _context.SaveChangesAsync();

        return new ServiceResponse<bool>
        {
            Data = true,
            Message = "Cart item quantity has been updated.",
        };
    }

    public async Task<ServiceResponse<bool>> RemoveItemFromCart(int productId, int productTypeId)
    {
        var dbCartItem = await _context.CartItems
            .FirstOrDefaultAsync(ci => ci.ProductId == productId
                && ci.ProductTypeId == productTypeId && ci.UserId == _authService.GetUserId()
            );
        if (dbCartItem is null)
        {
            return new ServiceResponse<bool>
            {
                Success = false,
                Data = false,
                Message = "Cart item does not exist.",
            };
        }

        _context.CartItems.Remove(dbCartItem);
        await _context.SaveChangesAsync();

        return new ServiceResponse<bool>
        {
            Data = true,
            Message = "Cart item has been removed.",
        };
    }
}
