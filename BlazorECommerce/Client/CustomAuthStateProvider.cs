using Blazored.LocalStorage;
using System.Security.Claims;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BlazorECommerce.Client;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    private readonly HttpClient _http;

    public CustomAuthStateProvider(ILocalStorageService localStorageService, HttpClient http)
    {
        _localStorageService = localStorageService;
        _http = http;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string accessToken = await _localStorageService.GetItemAsStringAsync("accessToken");

        var identity = new ClaimsIdentity();
        _http.DefaultRequestHeaders.Authorization = null;

        if (!string.IsNullOrEmpty(accessToken)) 
        {
            try 
            {
                identity = new ClaimsIdentity(ParseClaimsFromJwt(accessToken), "jwt");
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }
            catch
            {
                await _localStorageService.RemoveItemAsync("accessToken");
                identity = new ClaimsIdentity();
            }
        }

        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);

        NotifyAuthenticationStateChanged(Task.FromResult(state));

        return state;
    }

    private byte[] ParseBase64WithoutPadding(string base64String)
    {
        switch (base64String.Length % 4)
        {
            case 2:
                base64String += "==";
                break;
            case 3:
                base64String += "=";
                break;
        }

        return Convert.FromBase64String(base64String);
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string accessToken)
    {
        var payload = accessToken.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        var claims = keyValuePairs?.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));

        return claims;
    }
}
