using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HairArt.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class EmployeeController : Controller
{
    private readonly IServiceManager _serviceManager;

    public EmployeeController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public IActionResult Index()
    {
        var employees = _serviceManager.EmployeeService.GetAllEmployeesWithDetails(trackChanges: false);
        return View(employees);
    }

   [HttpGet]
public IActionResult EditEmployeeProducts(int id)
{
    // Çalışanı getir
    var employee = _serviceManager.EmployeeService.GetEmployeeById(id, false);
    if (employee == null)
    {
        return NotFound("Çalışan bulunamadı.");
    }

    // Tüm ürünleri ve atanmış ürünleri al
    var allProducts = _serviceManager.ProductService.GetAllProducts(false);
    var assignedProducts = _serviceManager.EmployeeProductService.GetProductsByEmployeeId(id, false)
                            .Select(ep => ep.ProductId)
                            .ToList();

    ViewBag.AllProducts = allProducts;
    ViewBag.AssignedProducts = assignedProducts;

    return View(employee);
}


[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult EditEmployeeProducts(int employeeId, List<int> productIds)
{
    // Atanmış ürünleri güncelle
    _serviceManager.EmployeeProductService.UpdateEmployeeProducts(employeeId, productIds);

    TempData["SuccessMessage"] = "Çalışanın ürünleri başarıyla güncellendi.";
    return RedirectToAction("Index");
}

[HttpGet]
public IActionResult Delete([FromRoute(Name = "id")] int id)
{
    try
    {
        // Çalışanın randevuları var mı kontrol et
        bool hasAppointments = _serviceManager.EmployeeService.HasAppointmentsForEmployee(id);

        if (hasAppointments)
        {
            // Randevusu olan çalışan için TempData ile uyarı mesajı ilet
            TempData["ErrorMessage"] = "Bu çalışan randevularla ilişkilendirilmiştir ve silinemez.";
            return RedirectToAction("Index");
        }

        // Eğer randevularla ilişkisi yoksa çalışanı sil
        _serviceManager.EmployeeService.DeleteEmployee(id);
        TempData["SuccessMessage"] = "Çalışan başarıyla silindi.";
    }
    catch (Exception ex)
    {
        // Hata durumunda kullanıcıya mesaj göster
        TempData["ErrorMessage"] = $"Çalışan silinirken bir hata oluştu: {ex.Message}";
    }

    return RedirectToAction("Index");
}


}
