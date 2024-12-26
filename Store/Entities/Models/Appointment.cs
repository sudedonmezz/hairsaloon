using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Models;

public class Appointment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AppointmentId { get; set; }

    [Required]
    [ForeignKey("User")]
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    [Required]
    public int EmployeeId { get; set; }

    [Required]
    public int ProductId { get; set; }
    public EmployeeProduct EmployeeProduct { get; set; } // Çalışan-Ürün ilişkisi

    [Required]
    public int ScheduleId { get; set; }
    public EmployeeSchedule EmployeeSchedule { get; set; } // Çalışan-Takvim ilişkisi

    [Required]
    public DateTime AppointmentDate { get; set; }
}
