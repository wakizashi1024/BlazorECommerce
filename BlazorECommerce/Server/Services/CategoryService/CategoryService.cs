﻿
namespace BlazorECommerce.Server.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly DataContext _context;

    public CategoryService(DataContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<IEnumerable<Category>>> GetCategoriesAsync()
    {
        var categories = await _context.Categories.ToListAsync();

        return new ServiceResponse<IEnumerable<Category>> 
        { 
            Data = categories 
        };
    }
}
