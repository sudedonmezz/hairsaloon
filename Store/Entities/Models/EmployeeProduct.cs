namespace Entities.Models;

public class EmployeeProduct
{
     public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public ICollection<Appointment>? Appointments { get; set; } // Çalışan-takvim randevuları
}
