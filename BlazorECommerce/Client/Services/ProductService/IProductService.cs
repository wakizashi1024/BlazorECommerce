namespace BlazorECommerce.Client.Services.ProductService;

public interface IProductService
{
    ICollection<Product> Products { get; set; }
    Task GetProducts();
}
