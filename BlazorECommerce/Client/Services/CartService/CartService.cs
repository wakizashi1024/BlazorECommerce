using Blazored.LocalStorage;
using System.Net.Http.Json;

namespace BlazorECommerce.Client.Services.CartService;

public class CartService : ICartService
{
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _http;
    private readonly AuthenticationStateProvider _authStateProvider;

    public event Action OnChange;

    public CartService(ILocalStorageService localStorage, HttpClient http, AuthenticationStateProvider authStateProvider)
    {
        _localStorage = localStorage;
        _http = http;
        _authStateProvider = authStateProvider;
    }

    private async Task<bool> IsUserAuthenticated() => (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;

    public async Task AddToCart(CartItem cartItem)
    {
        if (await IsUserAuthenticated())
        {
            await _http.PostAsJsonAsync("api/Cart/add", cartItem);
        }
        else
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
        }
        
        // OnChange.Invoke();
        await GetCartItemsCount();
    }

    private async Task<IEnumerable<CartItem>> GetCartItems()
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
        if (await IsUserAuthenticated())
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<IEnumerable<CartProductResponseDto>>>("api/Cart");
            return response.Data;
        }
        else
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
    }

    public async Task RemoveProductFromCart(int productId, int productTypeId)
    {
        if (await IsUserAuthenticated())
        {
            var response = await _http.DeleteAsync($"api/Cart/{productId}/{productTypeId}");
        }
        else
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
                // OnChange.Invoke();
            }
        }
    }

    public async Task UpdateQuantity(CartItem cartItem)
    {
        if (await IsUserAuthenticated())
        {
            var response = await _http.PutAsJsonAsync("api/Cart/update-quantity", cartItem);
        }
        else
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
            }
        }
        OnChange?.Invoke();
    }

    public async Task StoreCartItems(bool isEmptyLocalCart)
    {
        var localCart = await _localStorage.GetItemAsync<IEnumerable<CartItem>>("cart");
        if (localCart is null)
        {
            return;
        }

        await _http.PostAsJsonAsync("api/Cart", localCart);

        if (isEmptyLocalCart)
        {
            await _localStorage.RemoveItemAsync("cart");
        }
    }

    public async Task GetCartItemsCount()
    {
        if (await IsUserAuthenticated())
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<int>>("api/Cart/count");
            var count = result.Data;

            await _localStorage.SetItemAsync<int>("cartItemsCount", count);
        }
        else
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            await _localStorage.SetItemAsync<int>("cartItemsCount", cart is not null ? cart.Count : 0);
        }

        OnChange?.Invoke();
    }
}
