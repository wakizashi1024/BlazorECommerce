namespace BlazorECommerce.Client.Services.CartService;

public interface ICartService
{
    event Action OnChange;
    Task AddToCart(CartItem cartItem);
    Task<IEnumerable<CartItem>> GetCartItems();
    Task<IEnumerable<CartProductResponseDto>> GetCartProducts();
    Task RemoveProductFromCart(int productId, int productTypeId);
    Task UpdateQuantity(CartItem cartItem);
}
