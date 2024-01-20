
using JiebaNet.Segmenter;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq;
using System.Text.RegularExpressions;

namespace BlazorECommerce.Server.Services.ProductService;

public class ProductService : IProductService
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProductService(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
    {
        var response = new ServiceResponse<Product>();
        Product product = null;

        if (_httpContextAccessor.HttpContext.User.IsInRole("Admin"))
        {
            product = await _context.Products
                .Include(p => p.Variants.Where(pv => !pv.Deleted))
                .ThenInclude(pv => pv.ProductType)
                .FirstOrDefaultAsync(p => p.Id == productId && !p.Deleted);
        }
        else
        {
            product = await _context.Products
                .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
                .ThenInclude(pv => pv.ProductType)
                .FirstOrDefaultAsync(p => p.Id == productId && p.Visible && !p.Deleted);
        }
        
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
                    .Where(p => p.Visible && !p.Deleted)
                    .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
                    .ToListAsync(),
        };

        return response;
    }

    public async Task<ServiceResponse<IEnumerable<Product>>> GetProductsByCategoryAsync(string categoryUrl)
    {
        var response = new ServiceResponse<IEnumerable<Product>>()
        {
            Data = await _context.Products
                    .Where(p => p.Category != null 
                        && p.Category.Url.ToLower().Equals(categoryUrl.ToLower())
                        && p.Visible && !p.Deleted)
                    .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
                    .ToListAsync(),
        };

        return response;
    }

    public async Task<ServiceResponse<ProductSearchResultDto>> SearchProducts(string searchText, int page, int pageSize = 2)
    {
        var pageCount = Math.Ceiling((await FindProductBySearchText(searchText).CountAsync()) / (decimal)pageSize);
        var products = await FindProductBySearchText(searchText)
                            .Skip((page - 1) * (int)pageSize)
                            .Take(pageSize)
                            .ToListAsync();

        var response = new ServiceResponse<ProductSearchResultDto>
        {
            Data = new ProductSearchResultDto
            {
                Pages = (int)pageCount,
                Products = products,
                CurrentPage = page,
                PageSize = pageSize,
            }
        };

        return response;
    }

    public async Task<ServiceResponse<IEnumerable<string>>> GetProductSearchSuggestions(string searchText)
    {
        var products = await FindProductBySearchText(searchText).ToListAsync();

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

    public async Task<ServiceResponse<IEnumerable<Product>>> GetFeaturedProducts()
    {
        var response = new ServiceResponse<IEnumerable<Product>>
        {
            Data = await _context.Products
                    .Where(p => p.Featured && p.Visible && !p.Deleted)
                    .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
                    .ToListAsync()
        };

        return response;
    }
    private IIncludableQueryable<Product, IEnumerable<ProductVariant>> FindProductBySearchText(string searchText)
    {
        return _context.Products
                .Where(p => p.Title.ToLower().Contains(searchText.ToLower())
                    || p.Description.ToLower().Contains(searchText.ToLower())
                    && p.Visible && !p.Deleted
                )
                .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted));
    }

    public async Task<ServiceResponse<IEnumerable<Product>>> GetAdminProducts()
    {
        var response = new ServiceResponse<IEnumerable<Product>>
        {
            Data = await _context.Products
                    .Where(p => !p.Deleted)
                    .Include(p => p.Variants.Where(v => !v.Deleted))
                    .ThenInclude(pv => pv.ProductType)
                    .ToListAsync()
        };

        return response;
    }

    public async Task<ServiceResponse<Product>> CreateProduct(Product product)
    {
        foreach (var variant in product.Variants)
        {
            variant.ProductType = null;
        }
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return new ServiceResponse<Product>
        {
            Data = product,
        };
    }

    public async Task<ServiceResponse<Product>> UpdateProduct(Product product)
    {
        var dbProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
        if (dbProduct == null)
        {
            return new ServiceResponse<Product>
            {
                Success = false,
                Data = null,
                Message = "Product not found",
            };
        }

        dbProduct.Title = product.Title;
        dbProduct.Description = product.Description;
        dbProduct.ImageUrl = product.ImageUrl;
        dbProduct.CategoryId = product.CategoryId;
        dbProduct.Visible = product.Visible;

        foreach (var variant in product.Variants)
        {
            var dbVariant = await _context.ProductVariants
                .SingleOrDefaultAsync(pv => pv.ProductId == variant.ProductId
                && pv.ProductTypeId == variant.ProductTypeId);
            
            if (dbVariant is null)
            {
                // Create new one
                variant.ProductType = null;
                _context.ProductVariants.Add(variant);
            }
            else
            {
                dbVariant.ProductTypeId = variant.ProductTypeId;
                dbVariant.Price = variant.Price;
                dbVariant.OriginalPrice = variant.OriginalPrice;
                dbVariant.Visible = variant.Visible;
                dbVariant.Deleted = variant.Deleted;
                dbProduct.Featured = product.Featured;
            }
        }

        await _context.SaveChangesAsync();

        return new ServiceResponse<Product>
        {
            Data = product,
        };
    }

    public async Task<ServiceResponse<bool>> DeleteProduct(int productId)
    {
        var dbProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        if (dbProduct == null)
        {
            return new ServiceResponse<bool>
            {
                Success = false,
                Data = false,
                Message = "Product not found",
            };
        }

        dbProduct.Deleted = true;
        await _context.SaveChangesAsync();

        return new ServiceResponse<bool>
        {
            Data = true,
            Message = "Product has been deleted.",
        };
    }
}
