using Microsoft.AspNetCore.Components;

namespace BlazorECommerce.Client.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly HttpClient _http;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly NavigationManager _navigationManager;

    public OrderService(HttpClient http, AuthenticationStateProvider authStateProvider, NavigationManager navigationManager)
    {
        _http = http;
        _authStateProvider = authStateProvider;
        _navigationManager = navigationManager;
    }

    private async Task<bool> IsUserAuthenticated() => (await _authStateProvider.GetAuthenticationStateAsync())
        .User.Identity.IsAuthenticated;

    public async Task<string> PlaceOrder()
    {
        if (await IsUserAuthenticated())
        {
            // await _http.PostAsync("api/order", null);
            var result = await _http.PostAsync("api/Payment/checkout", null);
            var url = await result.Content.ReadAsStringAsync();

            return url;
        }
        else
        {
            //_navigationManager.NavigateTo("login");
            return "login";
        }
    }

    public async Task<IEnumerable<OrderOverviewResponseDto>> GetOrders()
    {
        var result = await _http.GetFromJsonAsync<ServiceResponse<IEnumerable<OrderOverviewResponseDto>>>("api/Order");

        return result.Data;
    }

    public async Task<OrderDetailsResponseDto> GetOrderDetails(int orderId)
    {
        var result = await _http.GetFromJsonAsync<ServiceResponse<OrderDetailsResponseDto>>($"api/Order/{orderId}");

        return result.Data;
    }
}
