using System.ComponentModel.DataAnnotations;
namespace Entities.Models;


public class ResetPasswordDto
{
    public string? Email { get; init; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage="Password is required.")]
    public string? Password { get; init;}

    [DataType(DataType.Password)]
    [Required(ErrorMessage="ConfirmPassword is required")]
    [Compare("Password",ErrorMessage="Password and ConfirmPassword must be match.")]
    public string? ConfirmPassword { get; init; }
}
