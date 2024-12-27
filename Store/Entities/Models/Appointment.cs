using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentId { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        
        public int? CategoryId { get; set; } // Nullable yapıldı
        public Category? Category { get; set; } // Navigation property


        [Required]
        public int ProductId { get; set; }
        public EmployeeProduct EmployeeProduct { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int ScheduleId { get; set; }
        public EmployeeSchedule EmployeeSchedule { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        public bool IsApproved { get; set; } = false;
    }
}
