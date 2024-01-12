
namespace BlazorECommerce.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ServiceResponse<int>> Register(UserRegisterDto request)
        {
            var result = await _http.PostAsJsonAsync("api/Auth/register", request);

            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }

        public async Task<ServiceResponse<string>> Login(UserLoginDto request)
        {
            var result = await _http.PostAsJsonAsync("api/Auth/login", request);

            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<bool>> ChangePassword(UserChangePasswordDto request)
        {
            var result = await _http.PostAsJsonAsync("api/Auth/change-password", request);

            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }
    }
}
