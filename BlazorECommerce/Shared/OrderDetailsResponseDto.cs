namespace BlazorECommerce.Shared;

public class OrderDetailsResponseDto
{
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public IEnumerable<OrderDetailsProductResponseDto> Products { get; set; }
    public OrderShipInfo ShipInfo { get; set; }
}
