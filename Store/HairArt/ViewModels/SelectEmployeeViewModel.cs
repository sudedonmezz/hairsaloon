using Entities.Models;
namespace HairArt.ViewModels
{
  public class SelectEmployeeViewModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } // Ürünün adı
    public IEnumerable<Employee> Employees { get; set; }
}

}
