using System.ComponentModel.DataAnnotations;
namespace Entities.Models;

public class UserDtoForUpdate : UserDto
{

    public HashSet<string> UserRoles { get; set; } =new HashSet<string>();

}
