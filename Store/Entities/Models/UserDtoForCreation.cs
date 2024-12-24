using System.ComponentModel.DataAnnotations;
namespace Entities.Models;

public class UserDtoForCreation : UserDto
{
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required.")]
    public string? Password { get; set; }
}
