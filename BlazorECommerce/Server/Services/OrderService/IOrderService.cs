namespace BlazorECommerce.Server.Services.OrderService;

public interface IOrderService
{
    Task<ServiceResponse<bool>> PlaceOrder();
    Task<ServiceResponse<IEnumerable<OrderOverviewResponseDto>>> GetOrders();
    Task<ServiceResponse<OrderDetailsResponseDto>> GetOrderDetails(int orderId);
}
