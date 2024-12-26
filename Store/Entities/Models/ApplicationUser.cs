namespace Entities.Models;

using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
    public string? LastName { get; set; }

    public ICollection<Appointment> Appointments { get; set; }
}
