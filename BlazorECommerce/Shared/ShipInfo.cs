using System.ComponentModel.DataAnnotations;

namespace BlazorECommerce.Shared;

public class ShipInfo
{
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Country { get; set; }
    public string State { get; set; } = string.Empty;
    [Required]
    public string City { get; set; }
    [Required]
    public string Line1 { get; set; }
    public string Line2 { get; set; } = string.Empty;
    [Required]
    public string PostalCode { get; set; }
    [Required]
    public string Phone { get; set; }

    public int UserId { get; set; }
}
