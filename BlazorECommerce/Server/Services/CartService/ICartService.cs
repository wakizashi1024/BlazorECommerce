namespace BlazorECommerce.Server.Services.CartService;

public interface ICartService
{
    Task<ServiceResponse<ICollection<CartProductResponseDto>>> GetCartProdcuts(IEnumerable<CartItem> cartItems);
    // Task<ServiceResponse<ICollection<CartProductResponseDto>>> StoreCartItems(ICollection<CartItem> cartItems, int userId);
    Task<ServiceResponse<ICollection<CartProductResponseDto>>> StoreCartItems(ICollection<CartItem> cartItems);
    Task<ServiceResponse<int>> GetCartItemsCount();
    Task<ServiceResponse<ICollection<CartProductResponseDto>>> GetDbCartProducts(int? userId = null);
    Task<ServiceResponse<bool>> AddToCart(CartItem cartItem);
    Task<ServiceResponse<bool>> UpdateQuantity(CartItem cartItem);
    Task<ServiceResponse<bool>> RemoveItemFromCart(int productId, int productTypeId);
}
