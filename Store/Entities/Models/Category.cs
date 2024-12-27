namespace Entities.Models;

public class Category
{
    public int CategoryId { get; set; }
    public String? CategoryName { get; set; }=String.Empty;

    public String? ImageUrl { get; set; } // Yeni Alan
    public String? CategoryDescription { get; set;}
  
    public ICollection<Product> Products { get; set; } //Collection navigation property
    
     public ICollection<Appointment>? Appointments { get; set; } // Çalışan-takvim randevuları
}
