
namespace BlazorECommerce.Server.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly DataContext _context;

    public CategoryService(DataContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<IEnumerable<Category>>> CreateCategoryAsync(Category category)
    {
        // category.Editing = category.IsNew = false;

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return await GetAdminCategoriesAsync();
    }

    public async Task<ServiceResponse<IEnumerable<Category>>> GetAdminCategoriesAsync()
    {
        var categories = await _context.Categories
        .Where(c => !c.Deleted)
        .ToListAsync();

        return new ServiceResponse<IEnumerable<Category>>
        {
            Data = categories
        };
    }
    public async Task<ServiceResponse<IEnumerable<Category>>> UpdateCategoryAsync(Category category)
    {
        var dbCategory = await GetCategoryById(category.Id);
        if (dbCategory is null)
        {
            return new ServiceResponse<IEnumerable<Category>>
            {
                Success = false,
                Message = "Category not found",
            };
        }

        dbCategory.Name = category.Name;
        dbCategory.Url = category.Url;
        dbCategory.Visible = category.Visible;

        await _context.SaveChangesAsync();

        return await GetAdminCategoriesAsync();
    }

    public async Task<ServiceResponse<IEnumerable<Category>>> DeleteCategoryAsync(int id)
    {
        Category category = await GetCategoryById(id);

        if (category is null)
        {
            return new ServiceResponse<IEnumerable<Category>>
            {
                Success = false,
                Message = "Category not found",
            };
        }

        category.Deleted = true;
        await _context.SaveChangesAsync();

        return await GetAdminCategoriesAsync();
    }

    public async Task<ServiceResponse<IEnumerable<Category>>> GetCategoriesAsync()
    {
        var categories = await _context.Categories
            .Where(c => !c.Deleted && c.Visible)
            .ToListAsync();

        return new ServiceResponse<IEnumerable<Category>> 
        { 
            Data = categories 
        };
    }

    private async Task<Category> GetCategoryById(int id)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }
}
