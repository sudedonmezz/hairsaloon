using Entities.Models;
namespace HairArt.ViewModels;

public class SelectProductViewModel
{
    public string CategoryName { get; set; } // Kategori adı
    public IEnumerable<Product> Products { get; set; }
}
