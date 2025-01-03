using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
namespace HairArt.Areas.Admin.Controllers;



[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ProductController : Controller
{

    private readonly IServiceManager _manager;

    public ProductController(IServiceManager manager)
  {
    _manager = manager;
}
    public IActionResult Index()
    {
        var model=_manager.ProductService.GetAllProducts(false);
        return View(model);
    }

    public IActionResult Create()
    {
        ViewBag.Categories=
        new SelectList(_manager.CategoryService.GetAllCategories(false),
        "CategoryId",
        "CategoryName",1);
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([FromForm]Product product)
    {
       
        if(ModelState.IsValid)
        {
             _manager.ProductService.CreateProduct(product);
        return RedirectToAction("Index");
        }
        return View();
       
       
    }

    public IActionResult Update([FromRoute(Name="id")] int id)
    {
         ViewBag.Categories=
        new SelectList(_manager.CategoryService.GetAllCategories(false),
        "CategoryId",
        "CategoryName",1);
        var model=_manager.ProductService.GetOneProduct(id,false);
        return View(model);
    }


[HttpPost]
[ValidateAntiForgeryToken]
    public IActionResult Update(Product product)
    {
        
        if(ModelState.IsValid)
        {
            _manager.ProductService.UpdateOneProduct(product);
        return RedirectToAction("Index");
        }
        return View();
        
    }

    [HttpGet]
public IActionResult Delete([FromRoute(Name = "id")] int id)
{
    // Ürünün randevularla ilişkilendirilip ilişkilendirilmediğini kontrol et
    bool hasAppointments = _manager.ProductService.HasAppointmentsForProduct(id);

    if (hasAppointments)
    {
        // TempData ile kullanıcıya mesaj ilet
        TempData["ErrorMessage"] = "Bu ürün, mevcut randevularla ilişkilendirilmiştir ve silinemez.";
        return RedirectToAction("Index");
    }

    // Eğer randevularla ilişkisi yoksa ürünü sil
    _manager.ProductService.DeleteOneProduct(id);
    TempData["SuccessMessage"] = "Ürün başarıyla silindi.";
    return RedirectToAction("Index");
}

}
