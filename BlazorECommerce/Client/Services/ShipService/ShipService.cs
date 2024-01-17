

using BlazorECommerce.Shared;

namespace BlazorECommerce.Client.Services.UserInfoService;

public class ShipService : IShipService
{
    private readonly HttpClient _http;

    public ShipService(HttpClient http)
    {
        _http = http;
    }

    public async Task<ShipInfo> CreateOrUpdateShipInfo(ShipInfo shipInfo)
    {
        var response = await _http.PostAsJsonAsync("api/Ship", shipInfo);

        return (await response.Content
            .ReadFromJsonAsync<ServiceResponse<ShipInfo>>()).Data;
    }

    public async Task<ShipInfo> GetShipInfo()
    {
        var response = await _http.GetFromJsonAsync<ServiceResponse<ShipInfo>>("api/Ship");

        return response.Data;
    }
}
