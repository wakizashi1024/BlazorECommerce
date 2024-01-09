namespace BlazorECommerce.Server.Services.CartService;

public interface ICartService
{
    Task<ServiceResponse<ICollection<CartProductResponseDto>>> GetCartProdcuts(IEnumerable<CartItem> cartItems);
}
