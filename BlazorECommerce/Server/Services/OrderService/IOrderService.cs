namespace BlazorECommerce.Server.Services.OrderService;

public interface IOrderService
{
    Task<ServiceResponse<bool>> PlaceOrder(int userId, string? remark = null);
    Task<ServiceResponse<IEnumerable<OrderOverviewResponseDto>>> GetOrders();
    Task<ServiceResponse<OrderDetailsResponseDto>> GetOrderDetails(int orderId);
    Task<ServiceResponse<OrderShipInfo>> AddShipInfo(OrderShipInfo orderShipInfo);
}
