namespace Entities.Models;

public class Schedule
{
    public int ScheduleId { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }

    public ICollection<EmployeeSchedule> EmployeeSchedules { get; set; } = new List<EmployeeSchedule>();
}
