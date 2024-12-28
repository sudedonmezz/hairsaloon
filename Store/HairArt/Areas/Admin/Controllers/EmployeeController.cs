using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Entities.Models;

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

[HttpGet]
public IActionResult Create()
{
    return View();
}

[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(Employee employee)
{
    if (ModelState.IsValid)
    {
        _serviceManager.EmployeeService.CreateEmployee(employee);
        TempData["SuccessMessage"] = "Çalışan başarıyla eklendi.";
        return RedirectToAction("Index");
    }

    TempData["ErrorMessage"] = "Çalışan eklenirken bir hata oluştu.";
    return View(employee);
}

[HttpGet]
public IActionResult EditEmployeeSchedules(int id)
{
    // Çalışanı getir
    var employee = _serviceManager.EmployeeService.GetEmployeeById(id, false);
    if (employee == null)
    {
        return NotFound("Employee not found.");
    }

    // Tüm takvimleri getir
    var allSchedules = _serviceManager.ScheduleService.GetAllSchedules(false);
    if (allSchedules == null || !allSchedules.Any())
    {
        TempData["ErrorMessage"] = "No schedules available.";
        return RedirectToAction("Index");
    }

    // Çalışana atanmış takvimleri getir
    var assignedSchedules = _serviceManager.EmployeeScheduleService
        .GetSchedulesByEmployee(id, false)
        .Select(es => es.ScheduleId)
        .ToList();

    // ViewBag üzerinden takvimleri ve atanmış takvimleri gönder
    ViewBag.AllSchedules = allSchedules;
    ViewBag.AssignedSchedules = assignedSchedules;

    return View(employee);
}


[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult EditEmployeeSchedules(int employeeId, List<int> scheduleIds)
{
    if (scheduleIds == null)
    {
        TempData["ErrorMessage"] = "No schedules selected.";
        return RedirectToAction("EditEmployeeSchedules", new { id = employeeId });
    }

    try
    {
        // Çalışanın takvimlerini güncelle
        _serviceManager.EmployeeScheduleService.UpdateEmployeeSchedules(employeeId, scheduleIds);

        TempData["SuccessMessage"] = "Çalışanın takvimleri başarıyla güncellendi.";
    }
    catch (Exception ex)
    {
        TempData["ErrorMessage"] = $"Takvim güncellenirken bir hata oluştu: {ex.Message}";
    }

    return RedirectToAction("Index");
}





}
