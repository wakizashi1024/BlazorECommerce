using Blazored.LocalStorage;

namespace BlazorECommerce.Client.Services.CartService;

public class CartService : ICartService
{
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _http;

    public event Action OnChange;

    public CartService(ILocalStorageService localStorage, HttpClient http)
    {
        _localStorage = localStorage;
        _http = http;
    }

    public async Task AddToCart(CartItem cartItem)
    {
        var cart = await _localStorage.GetItemAsync<ICollection<CartItem>>("cart");
        if (cart == null)
        {
            cart = new List<CartItem>();
        }
        var sameItem = cart.FirstOrDefault(x => x.ProductId == cartItem.ProductId && x.ProductTypeId == cartItem.ProductTypeId);
        if (sameItem == null)
        {
            cart.Add(cartItem);
        }
        else
        {
            sameItem.Quantity += cartItem.Quantity;
        }

        await _localStorage.SetItemAsync<ICollection<CartItem>>("cart", cart);
        OnChange.Invoke();
    }

    public async Task<IEnumerable<CartItem>> GetCartItems()
    {
        var cart = await _localStorage.GetItemAsync<ICollection<CartItem>>("cart");
        if (cart == null)
        {
            cart = new List<CartItem>();
        }

        return cart;
    }

    public async Task<IEnumerable<CartProductResponseDto>> GetCartProducts()
    {
        var cartItems = await GetCartItems();
        var response = await _http.PostAsJsonAsync("api/Cart/products", cartItems);
        var cartProducts = await response.Content.ReadFromJsonAsync<ServiceResponse<IEnumerable<CartProductResponseDto>>>();

        if (cartProducts is null || cartProducts.Data is null)
        {
            return Enumerable.Empty<CartProductResponseDto>();
        }

        return cartProducts.Data;
    }

    public async Task RemoveProductFromCart(int productId, int productTypeId)
    {
        var cart = await _localStorage.GetItemAsync<ICollection<CartItem>>("cart");
        if (cart == null)
        {
            return;
        }

        var cartItem = cart.FirstOrDefault(x => x.ProductId == productId && x.ProductTypeId == productTypeId);
        if (cartItem is not null)
        {
            cart.Remove(cartItem);
            await _localStorage.SetItemAsync("cart", cart);
            OnChange.Invoke();
        }
    }

    public async Task UpdateQuantity(CartItem cartItem)
    {
        var cart = await _localStorage.GetItemAsync<ICollection<CartItem>>("cart");
        if (cart == null)
        {
            return;
        }

        var _cartItem = cart.FirstOrDefault(x => x.ProductId == cartItem.ProductId && x.ProductTypeId == cartItem.ProductTypeId);
        if (_cartItem is not null)
        {
            _cartItem.Quantity = cartItem.Quantity;
            await _localStorage.SetItemAsync("cart", cart);
            OnChange.Invoke();
        }
    }
}
