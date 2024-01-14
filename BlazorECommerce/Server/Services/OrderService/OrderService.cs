namespace BlazorECommerce.Server.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly DataContext _context;
    private readonly ICartService _cartService;
    private readonly IAuthService _authService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OrderService(DataContext context, ICartService cartService, IAuthService authService)
    {
        _context = context;
        _cartService = cartService;
        _authService = authService;
    }

    public async Task<ServiceResponse<OrderDetailsResponseDto>> GetOrderDetails(int orderId)
    {
        var response = new ServiceResponse<OrderDetailsResponseDto>();
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.ProductType)
            .Where(o => o.UserId == _authService.GetUserId() && o.Id == orderId)
            .OrderByDescending(o => o.OrderDate)
            .FirstOrDefaultAsync();

        if (order == null)
        {
            response.Success = false;
            response.Message = "Order not found.";
            return response;
        }

        var orderDetailsResponse = new OrderDetailsResponseDto
        {
            OrderDate = order.OrderDate,
            TotalPrice = order.TotalPrice,
            Products = order.OrderItems.Select(oi => new OrderDetailsProductResponseDto
            {
                ProductId = oi.ProductId,
                Title = oi.Product.Title,
                ProductType = oi.ProductType.Name,
                ImageUrl = oi.Product.ImageUrl,
                Quantity = oi.Quantity,
                TotalPrice = oi.TotalPrice,
            }).ToList(),
        };

        response.Data = orderDetailsResponse;
        
        return response;
    }

    public async Task<ServiceResponse<IEnumerable<OrderOverviewResponseDto>>> GetOrders()
    {
        var response = new ServiceResponse<IEnumerable<OrderOverviewResponseDto>>();

        var orders = await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .Where(o => o.UserId == _authService.GetUserId())
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();

        var orderResponse = orders.Select(o => new OrderOverviewResponseDto
        {
            Id = o.Id,
            OrderDate = o.OrderDate,
            TotalPrice = o.TotalPrice,
            Product = o.OrderItems.Count > 1
                ? $"{o.OrderItems.First().Product.Title} and {o.OrderItems.Count - 1} more..."
                : o.OrderItems.First().Product.Title,
            ProductImageUrl = o.OrderItems.First().Product.ImageUrl
        });

        response.Data = orderResponse;

        return response;
    }

    public async Task<ServiceResponse<bool>> PlaceOrder()
    {
        var userId = _authService.GetUserId();
        var products = (await _cartService.GetDbCartProducts()).Data;
        
        var orderItems = new List<OrderItem>();
        decimal totalPrice = 0;
        foreach (var product in products)
        {
            totalPrice += product.Price * product.Quantity;
            orderItems.Add(new OrderItem
            {
                ProductId = product.ProductId,
                ProductTypeId = product.ProductTypeId,
                Quantity = product.Quantity,
                TotalPrice = product.Price * product.Quantity
            });
        }

        var order = new Order
        {
            UserId = userId,
            OrderItems = orderItems,
            TotalPrice = totalPrice,
            OrderDate = DateTime.Now,
        };

        _context.Orders.Add(order);

        _context.CartItems.RemoveRange(
            _context.CartItems.Where(ci => ci.UserId == userId
        ));

        await _context.SaveChangesAsync();

        return new ServiceResponse<bool>
        {
            Data = true,
        };
    }
}
