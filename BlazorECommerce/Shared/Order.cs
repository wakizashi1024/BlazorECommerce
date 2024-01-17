using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorECommerce.Shared;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }
    public string Remark { get; set; } = string.Empty;
    
    public List<OrderItem> OrderItems { get; set; }
    public OrderShipInfo? ShipInfo { get; set; }
}
