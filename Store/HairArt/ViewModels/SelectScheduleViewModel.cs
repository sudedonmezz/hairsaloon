namespace HairArt.ViewModels
{
    public class SelectScheduleViewModel
    {
        public int ProductId { get; set; }
        public int EmployeeId { get; set; }

         public string EmployeeName { get; set; } // Çalışan adı
        public IEnumerable<Entities.Models.Schedule> Schedules { get; set; }
    }
}
