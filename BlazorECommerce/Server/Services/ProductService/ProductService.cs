
using JiebaNet.Segmenter;
using System.Text.RegularExpressions;

namespace BlazorECommerce.Server.Services.ProductService;

public class ProductService : IProductService
{
    private readonly DataContext _context;

    public ProductService(DataContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
    {
        var response = new ServiceResponse<Product>();
        var product = await _context.Products
            .Include(p => p.Variants)
            .ThenInclude(pv => pv.ProductType)
            .FirstOrDefaultAsync(p => p.Id == productId);
        if (product == null) {
            response.Success = false;
            response.Message = "Product does not exist.";
        }
        else
        {
            response.Data = product;
        }

        return response;
    }

    public async Task<ServiceResponse<IEnumerable<Product>>> GetProductsAsync()
    {
        var response = new ServiceResponse<IEnumerable<Product>>()
        {
            Data = await _context.Products
                    .Include(p => p.Variants)
                    .ToListAsync(),
        };

        return response;
    }

    public async Task<ServiceResponse<IEnumerable<Product>>> GetProductsByCategoryAsync(string categoryUrl)
    {
        var response = new ServiceResponse<IEnumerable<Product>>()
        {
            Data = await _context.Products
                    .Where(p => p.Category != null && p.Category.Url.ToLower().Equals(categoryUrl.ToLower()))
                    .Include(p => p.Variants)
                    .ToListAsync(),
        };

        return response;
    }

    public async Task<ServiceResponse<IEnumerable<Product>>> SearchProducts(string searchText)
    {
        var response = new ServiceResponse<IEnumerable<Product>>
        {
            Data = await FindProductBySearchText(searchText),
        };

        return response;
    }

    public async Task<ServiceResponse<IEnumerable<string>>> GetProductSearchSuggestions(string searchText)
    {
        var products = await FindProductBySearchText(searchText);

        ICollection<string> result = new List<string>();

        foreach(var product in products)
        {
            if (product.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            {
                result.Add(product.Title);
            }

            if (product.Description is not null && searchText.Length > 1)
            {
                var punctuation = product.Description
                    .Where(char.IsPunctuation)
                    .Distinct()
                    .ToArray();
                // If description has any non eng or charactor, Use Jieba split words.
                var words = Regex.IsMatch(new string(product.Description.Where(c => !char.IsPunctuation(c)).ToArray()), @"^[a-zA-Z0-9\s]+$")
                        ? product.Description.Split()
                            .Select(s => s.Trim(punctuation))
                        : new JiebaSegmenter().CutForSearch(product.Description)
                            .Select(s => s.Trim(punctuation));
                foreach (var word in words)
                {
                    if (word.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                        && !result.Contains(word))
                    {
                        result.Add(word);
                    }
                }
            }
        }

        return new ServiceResponse<IEnumerable<string>> 
        { 
            Data = result 
        };
    }

    private async Task<IEnumerable<Product>> FindProductBySearchText(string searchText)
    {
        return await _context.Products
                .Where(p => p.Title.ToLower().Contains(searchText.ToLower())
                    || p.Description.ToLower().Contains(searchText.ToLower())
                )
                .Include(p => p.Variants)
                .ToListAsync();
    }
}
