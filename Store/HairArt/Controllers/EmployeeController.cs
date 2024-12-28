using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Services.Contracts;

namespace HairArt.Controllers;

public class EmployeeController : Controller
{
    private readonly IServiceManager _serviceManager;

    public EmployeeController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    // Müsait Çalışanları Listele
    public IActionResult AvailableEmployees()
{
    var availableEmployees = _serviceManager.EmployeeService
        .GetAllEmployees(trackChanges: false)
        .Where(e => e.IsAvailable)
        .ToList();

    return View(availableEmployees);
}

public IActionResult Index()
    {
        var employees = _serviceManager.EmployeeService.GetAllEmployeesWithDetails(false);
        return View(employees);
    }

// Kategoriye göre çalışanları getiren action
    public IActionResult GetByCategory(int categoryId)
    {
        // Sadece available (IsAvailable = true) çalışanları getiriyoruz
        ViewBag.ProductId = categoryId;
    var employees = _serviceManager.EmployeeService
        .GetEmployeesByCategoryId(categoryId, trackChanges: false)
        .Where(e => e.IsAvailable)
        .ToList();

    if (!employees.Any())
    {
        ViewBag.Message = "Bu kategori için müsait çalışan bulunamadı.";
        return View("NoEmployees"); // Çalışan yoksa özel bir sayfa gösterir.
    }

    return View(employees); // Sadece müsait çalışanları döndürür.
    }

    // Ürün bazlı çalışanları getiren action
 public IActionResult GetByProduct(int productId)
{
    // Ürüne ait CategoryId'yi sorgula
    var product = _serviceManager.ProductService.GetOneProduct(productId, trackChanges: false);
    if (product == null)
    {
        return NotFound("Ürün bulunamadı.");
    }

    var employees = _serviceManager.EmployeeService
        .GetEmployeesByProductId(productId, trackChanges: false)
        .Where(e => e.IsAvailable)
        .ToList();

    if (!employees.Any())
    {
        ViewBag.Message = "Uygun çalışan bulunmamaktadır.";
        ViewBag.CategoryId = product.CategoryId; // Ürüne ait CategoryId
        return View("NoEmployees");
    }

    return View("GetByProduct", employees);
}

// EmployeeController.cs
[HttpGet]
public IActionResult ViewEmployeeSchedules()
{
    var employeeSchedules = _serviceManager.EmployeeScheduleService
        .GetAllEmployeeSchedules(trackChanges: false)
        .Where(es => es.Employee != null && es.Schedule != null) // Null kontrolleri
        .Select(es => new
        {
            es.Employee.EmployeeId,
            es.Employee.FirstName,
            es.Employee.LastName,
            es.Schedule.ScheduleId,
            es.Schedule.StartDateTime,
            es.Schedule.EndDateTime
        })
        .ToList();

    return View(employeeSchedules);
}






}
