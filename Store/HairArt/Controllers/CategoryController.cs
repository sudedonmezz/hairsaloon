using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
namespace HairArt.Controllers;

public class CategoryController : Controller
{
    private IRepositoryManager _manager;

    public CategoryController(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public IActionResult Index()
    {
        var model=_manager.Category.FindAll(false);
        return View(model);
    }

    // Yeni Metot: Belirli bir kategorinin ürünlerini listele
    public IActionResult Products(int id)
    {
        var category = _manager.Category.FindByCondition(c => c.CategoryId == id, false).SingleOrDefault();
        if (category == null)
        {
            return NotFound();
        }

        var products = _manager.Product.FindByCondition(p => p.CategoryId == id, false).ToList();
        ViewBag.CategoryName = category.CategoryName;
        return View(products);
    }
}
