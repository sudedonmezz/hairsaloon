namespace HairArt.ViewModels
{
    public class SelectScheduleViewModel
    {
        public int ProductId { get; set; }
        public int EmployeeId { get; set; }
        public IEnumerable<Entities.Models.Schedule> Schedules { get; set; }
    }
}
