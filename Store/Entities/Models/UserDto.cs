using System.ComponentModel.DataAnnotations;
namespace Entities.Models;

public class UserDto
{
    [DataType(DataType.Text)]
    [Required(ErrorMessage="Name is required")]
    public string? Name{ get; init; }

[DataType(DataType.Text)]
[Required(ErrorMessage="LastName is required")]
    public string? LastName { get; init; }

[DataType(DataType.EmailAddress)]
[EmailAddress(ErrorMessage = "Invalid email format.")]
    public string? Email { get; init; }


[DataType(DataType.PhoneNumber)]
[Required(ErrorMessage="PhoneNumber is required")]
    public string? PhoneNumber { get; init; }

    public HashSet<string>Roles { get; set; }=new HashSet<string>();
}
