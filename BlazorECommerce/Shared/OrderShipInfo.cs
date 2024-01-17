using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorECommerce.Shared;

public class OrderShipInfo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string State { get; set; } = string.Empty;
    public string City { get; set; }
    public string Line1 { get; set; }
    public string Line2 { get; set; } = string.Empty;
    public string PostalCode { get; set; }
    public string Phone { get; set; }
    public string Remark { get; set; } = string.Empty;

    [JsonIgnore]
    public Order? Order { get; set; }
    public int? OrderId { get; set; }
}
