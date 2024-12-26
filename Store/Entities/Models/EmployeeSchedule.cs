using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Models;

public class EmployeeSchedule
{
      [Key, Column(Order = 0)]
     public int EmployeeId { get; set; }
    public Employee? Employee { get; set; } // Navigation property

     [Key, Column(Order = 1)]
    public int ScheduleId { get; set; }
    public Schedule? Schedule { get; set; } // Navigation property

     public ICollection<Appointment>? Appointments { get; set; } // Çalışan-takvim randevuları
}
