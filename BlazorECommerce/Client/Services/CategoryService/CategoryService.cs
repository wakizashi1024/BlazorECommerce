

namespace BlazorECommerce.Client.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _http;

        public event Action OnChange;

        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public ICollection<Category> AdminCategories { get; set; } = new List<Category>();

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

        public async Task GetAdminCategories()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<ICollection<Category>>>("api/Category/admin");
            if (response is not null && response.Data != null)
            {
                AdminCategories = response.Data;
            }
        }

        public async Task CreateCategory(Category category)
        {
            var response = await _http.PostAsJsonAsync("api/Category/admin", category);
            AdminCategories = (await response.Content
                    .ReadFromJsonAsync<ServiceResponse<ICollection<Category>>>()
                ).Data;
            await GetCategories();

            OnChange?.Invoke();
        }

        public async Task DeleteCategory(int categoryId)
        {
            var response = await _http.DeleteAsync($"api/Category/admin/{categoryId}");
            AdminCategories = (await response.Content
                    .ReadFromJsonAsync<ServiceResponse<ICollection<Category>>>()
                ).Data;
            await GetCategories();

            OnChange?.Invoke();
        }

        public async Task UpdateCategory(Category category)
        {
            var response = await _http.PutAsJsonAsync("api/Category/admin", category);
            AdminCategories = (await response.Content
                    .ReadFromJsonAsync<ServiceResponse<ICollection<Category>>>()
                ).Data;
            await GetCategories();

            OnChange?.Invoke();
        }
        public Category GenNewCategory()
        {
            var newCategory = new Category
            {
                IsNew = true,
                Editing = true,
            };
            AdminCategories.Add(newCategory);
            
            OnChange?.Invoke();
            return newCategory;
        }
    }
}
