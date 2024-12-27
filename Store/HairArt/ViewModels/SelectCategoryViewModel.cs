using Entities.Models;
namespace HairArt.ViewModels;

public class SelectCategoryViewModel
{
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string UserPhoneNumber { get; set; }
    public IEnumerable<Category> Categories { get; set; }
}
