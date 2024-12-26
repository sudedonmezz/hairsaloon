namespace Entities.Models;

public class Employee
{
public int EmployeeId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool IsAvailable { get; set; } = true;

    

    public ICollection<EmployeeProduct> EmployeeProducts { get; set; } = new List<EmployeeProduct>(); // Many-to-Many Relationship
    public ICollection<EmployeeSchedule> EmployeeSchedules { get; set; } = new List<EmployeeSchedule>();
}
