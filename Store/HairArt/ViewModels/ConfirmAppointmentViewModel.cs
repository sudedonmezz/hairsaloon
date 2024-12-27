using Entities.Models;
namespace HairArt.ViewModels
{
  public class ConfirmAppointmentViewModel
    {
        public string UserName { get; set; } // Kullanıcı adı ve soyadı
        public int CategoryId { get; set; } // Kategori ID
        public Category Category { get; set; } // Kategori Bilgisi
        public int ProductId { get; set; } // Ürün ID
        public Product Product { get; set; } // Ürün Bilgisi
        public int EmployeeId { get; set; } // Çalışan ID
        public Employee Employee { get; set; } // Çalışan Bilgisi
        public int ScheduleId { get; set; } // Takvim ID
        public Schedule Schedule { get; set; } // Takvim Bilgisi
        public DateTime AppointmentDate { get; set; } // Randevu Tarihi
    }

}
