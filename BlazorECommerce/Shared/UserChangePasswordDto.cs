using System.ComponentModel.DataAnnotations;

namespace BlazorECommerce.Shared;

public class UserChangePasswordDto
{
    [Required, StringLength(100, MinimumLength = 8)]
    public string OldPassword { get; set; } = string.Empty;
    [Required, StringLength(100, MinimumLength = 8)]
    public string NewPassword { get; set; } = string.Empty;
    [Required, Compare("NewPassword", ErrorMessage = "The passwords do not match.")]
    public string ConfirmNewPassword { get; set; } = string.Empty;
}
