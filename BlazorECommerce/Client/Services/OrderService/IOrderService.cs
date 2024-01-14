namespace BlazorECommerce.Client.Services.OrderService;

public interface IOrderService
{
    Task PlaceOrder();
    Task <IEnumerable<OrderOverviewResponseDto>> GetOrders();
    Task<OrderDetailsResponseDto> GetOrderDetails(int orderId);
}
