
namespace BlazorECommerce.Client.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _http;

        public ICollection<Category> Categories { get; set; } = new List<Category>();

        public CategoryService(HttpClient http)
        {
            _http = http;
        }

        public async Task GetCategories()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<ICollection<Category>>>("api/Category");
            if (result is not null && result.Data is not null)
            {
                Categories = result.Data;
            }
        }
    }
}
