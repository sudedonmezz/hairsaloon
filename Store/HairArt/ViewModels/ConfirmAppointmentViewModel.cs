namespace HairArt.ViewModels
{
    public class ConfirmAppointmentViewModel
    {
        public Entities.Models.Product Product { get; set; }
        public Entities.Models.Employee Employee { get; set; }
        public Entities.Models.Schedule Schedule { get; set; }
        public DateTime AppointmentDate { get; set; }

        public string SelectedTime { get; set; } // Kullanıcının seçtiği saat


        public string UserName { get; set; } // Bu satırı ekleyin
    }
}
