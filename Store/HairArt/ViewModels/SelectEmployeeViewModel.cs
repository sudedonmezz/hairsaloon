namespace HairArt.ViewModels
{
    public class SelectEmployeeViewModel
    {
        public int ProductId { get; set; }
        public IEnumerable<Entities.Models.Employee> Employees { get; set; }
    }
}
