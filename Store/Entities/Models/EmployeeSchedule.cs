namespace Entities.Models;

public class EmployeeSchedule
{
     public int EmployeeId { get; set; }
    public Employee? Employee { get; set; } // Navigation property

    public int ScheduleId { get; set; }
    public Schedule? Schedule { get; set; } // Navigation property
}
