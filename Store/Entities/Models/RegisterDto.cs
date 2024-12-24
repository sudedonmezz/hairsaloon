using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public record RegisterDto
{

    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }
 [Required(ErrorMessage = "LastName is required")]
    public string? LastName { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string? Email { get; set; }
 [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }

    [Phone(ErrorMessage = "Invalid phone number format.")]
    [Required(ErrorMessage = "Phone number is required")]
    public string? PhoneNumber { get; set; }
}
