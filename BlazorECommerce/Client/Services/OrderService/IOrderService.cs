namespace BlazorECommerce.Client.Services.OrderService;

public interface IOrderService
{
    Task<string> PlaceOrder();
    Task <IEnumerable<OrderOverviewResponseDto>> GetOrders();
    Task<OrderDetailsResponseDto> GetOrderDetails(int orderId);
}
