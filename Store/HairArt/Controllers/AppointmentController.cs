using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Entities.Models;
using HairArt.ViewModels;

namespace HairArt.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AppointmentController(IServiceManager serviceManager, UserManager<ApplicationUser> userManager)
        {
            _serviceManager = serviceManager;
            _userManager = userManager;
        }

        // 1. Kategori seçimi
        [HttpGet]
        public async Task<IActionResult> SelectCategory()
        {
            var user = await _userManager.GetUserAsync(User);
            var categories = _serviceManager.CategoryService.GetAllCategories(false);

            var viewModel = new SelectCategoryViewModel
            {
                UserName = $"{user.Name} {user.LastName}",
                UserEmail = user.Email,
                UserPhoneNumber = user.PhoneNumber,
                Categories = categories
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SelectCategory(int categoryId)
        {
            return RedirectToAction("SelectProduct", new { categoryId });
        }

        // 2. Ürün seçimi
        [HttpGet]
        public IActionResult SelectProduct(int categoryId)
        {
            var products = _serviceManager.ProductService.GetProductsByCategoryId(categoryId, false);

            var viewModel = new SelectProductViewModel
            {
                Products = products
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SelectProduct(int categoryId, int productId)
        {
            return RedirectToAction("SelectEmployee", new { categoryId, productId });
        }

        // 3. Çalışan seçimi
        [HttpGet]
        public IActionResult SelectEmployee(int categoryId, int productId)
        {
            var employees = _serviceManager.EmployeeProductService.GetEmployeesByProductId(productId, false)
                .Where(ep => ep.Employee != null && ep.Employee.IsAvailable)
                .Select(ep => ep.Employee)
                .ToList();

            var viewModel = new SelectEmployeeViewModel
            {
                ProductId = productId,
                Employees = employees
            };

            ViewBag.CategoryId = categoryId;
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SelectEmployee(int categoryId, int productId, int employeeId)
        {
            return RedirectToAction("SelectSchedule", new { categoryId, productId, employeeId });
        }

        // 4. Takvim seçimi
        [HttpGet]
        public IActionResult SelectSchedule(int categoryId, int productId, int employeeId)
        {
            var schedules = _serviceManager.EmployeeScheduleService.GetSchedulesByEmployee(employeeId, false)
                .Select(es => es.Schedule)
                .ToList();

            var viewModel = new SelectScheduleViewModel
            {
                ProductId = productId,
                EmployeeId = employeeId,
                Schedules = schedules
            };

            ViewBag.CategoryId = categoryId;
            return View(viewModel);
        }

   [HttpPost]
public IActionResult SelectSchedule(int categoryId, int productId, int employeeId, int scheduleId, string appointmentTime)
{
    // Gelen saat bilgisini doğrulayın
    if (string.IsNullOrEmpty(appointmentTime))
    {
        TempData["ErrorMessage"] = "Lütfen bir saat seçin.";
        return RedirectToAction("SelectSchedule", new { categoryId, productId, employeeId });
    }

    // ConfirmAppointment'a yönlendirme
    return RedirectToAction("ConfirmAppointment", new { categoryId, productId, employeeId, scheduleId, appointmentTime });
}

[HttpGet]
public async Task<IActionResult> ConfirmAppointment(int categoryId, int productId, int employeeId, int scheduleId, string appointmentTime)
{
    var category = _serviceManager.CategoryService.GetCategoryById(categoryId, false);
    var product = _serviceManager.ProductService.GetOneProduct(productId, false);
    var employee = _serviceManager.EmployeeService.GetEmployeeById(employeeId, false);
    var schedule = _serviceManager.ScheduleService.GetScheduleById(scheduleId, false);
    var user = await _userManager.GetUserAsync(User);

    if (category == null || product == null || employee == null || schedule == null)
    {
        return RedirectToAction("Error");
    }

    // Saat bilgisini Schedule tarihine ekleyin
    DateTime appointmentDateTime;
    try
    {
        var timeParts = appointmentTime.Split(':');
        appointmentDateTime = schedule.StartDateTime.Date
            .AddHours(int.Parse(timeParts[0]))
            .AddMinutes(int.Parse(timeParts[1]));
    }
    catch
    {
        TempData["ErrorMessage"] = "Geçersiz saat formatı.";
        return RedirectToAction("SelectSchedule", new { categoryId, productId, employeeId });
    }

    var viewModel = new ConfirmAppointmentViewModel
    {
        UserName = $"{user.Name} {user.LastName}",
        Category = category,
        Product = product,
        Employee = employee,
        Schedule = schedule,
        AppointmentDate = appointmentDateTime
    };

    return View(viewModel);
}





[HttpPost]
public IActionResult ConfirmAppointment(ConfirmAppointmentViewModel model)
{
    var user = _userManager.GetUserAsync(User).Result;
    if (user == null)
    {
        return RedirectToAction("Error");
    }

    // Schedule bilgisine erişin
    var schedule = _serviceManager.ScheduleService.GetScheduleById(model.ScheduleId, false);
    if (schedule == null)
    {
        TempData["ErrorMessage"] = "Seçilen takvim bulunamadı.";
        return RedirectToAction("SelectSchedule", new { model.CategoryId, model.ProductId, model.EmployeeId });
    }

    // Girilen saat ve takvim tarihini birleştirerek randevu tarihini oluşturun
    DateTime appointmentDateTime;
    try
    {
        var timeParts = model.AppointmentDate.ToString("HH:mm").Split(':');
        appointmentDateTime = schedule.StartDateTime.Date
            .AddHours(int.Parse(timeParts[0]))
            .AddMinutes(int.Parse(timeParts[1]));
    }
    catch
    {
        TempData["ErrorMessage"] = "Geçersiz saat formatı.";
        return RedirectToAction("SelectSchedule", new { model.CategoryId, model.ProductId, model.EmployeeId });
    }

    // Randevuyu oluştur ve kaydet
    var appointment = new Appointment
    {
        UserId = user.Id,
        EmployeeId = model.EmployeeId,
        ProductId = model.ProductId,
        ScheduleId = model.ScheduleId,
        CategoryId = model.CategoryId,
        AppointmentDate = appointmentDateTime // Doğru tarih ve saat
    };

    _serviceManager.AppointmentService.CreateAppointment(appointment);

    TempData["SuccessMessage"] = "Randevu başarıyla oluşturuldu.";
    return RedirectToAction("AppointmentSuccess");
}


        // Başarılı randevu
        [HttpGet]
        public IActionResult AppointmentSuccess()
        {
            return View();
        }
    }
}
