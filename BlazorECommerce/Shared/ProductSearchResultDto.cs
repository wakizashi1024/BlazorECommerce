namespace BlazorECommerce.Shared
{
    public class ProductSearchResultDto
    {
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
