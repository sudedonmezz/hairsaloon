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

        // 1. Ürün seçimi
        [HttpGet]
        public async Task<IActionResult> SelectProduct()
        {
            var user = await _userManager.GetUserAsync(User);
            var products = _serviceManager.ProductService.GetAllProducts(false);

            if (user == null || products == null)
            {
                return RedirectToAction("Error");
            }

            var viewModel = new SelectProductViewModel
            {
                UserName = $"{user.Name} {user.LastName}",
                UserEmail = user.Email,
                UserPhoneNumber = user.PhoneNumber,
                Products = products
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SelectProduct(int productId)
        {
            return RedirectToAction("SelectEmployee", new { productId });
        }

        // 2. Çalışan seçimi
        [HttpGet]
        public IActionResult SelectEmployee(int productId)
        {
            var employees = _serviceManager.EmployeeProductService.GetEmployeesByProductId(productId, false)
                .Where(ep => ep.Employee != null && ep.Employee.IsAvailable)
                .Select(ep => ep.Employee)
                .ToList();

            if (employees == null || !employees.Any())
            {
                return RedirectToAction("Error");
            }

            var viewModel = new SelectEmployeeViewModel
            {
                ProductId = productId,
                Employees = employees
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SelectEmployee(int productId, int employeeId)
        {
            return RedirectToAction("SelectSchedule", new { productId, employeeId });
        }

        // 3. Takvim seçimi
        [HttpGet]
        public IActionResult SelectSchedule(int productId, int employeeId)
        {
            var schedules = _serviceManager.EmployeeScheduleService.GetSchedulesByEmployee(employeeId, false)
                .Select(es => es.Schedule)
                .ToList();

            if (schedules == null || !schedules.Any())
            {
                return RedirectToAction("Error");
            }

            var viewModel = new SelectScheduleViewModel
            {
                ProductId = productId,
                EmployeeId = employeeId,
                Schedules = schedules
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SelectSchedule(int productId, int employeeId, int scheduleId)
        {
            return RedirectToAction("ConfirmAppointment", new { productId, employeeId, scheduleId });
        }

    [HttpGet]
public async Task<IActionResult> ConfirmAppointment(int productId, int employeeId, int scheduleId)
{
    var product = _serviceManager.ProductService.GetOneProduct(productId, false);
    var employee = _serviceManager.EmployeeService.GetEmployeeById(employeeId, false);
    var schedule = _serviceManager.ScheduleService.GetScheduleById(scheduleId, false);
    var user = await _userManager.GetUserAsync(User);

    if (product == null || employee == null || schedule == null || user == null)
    {
        return RedirectToAction("Error");
    }

    var viewModel = new ConfirmAppointmentViewModel
    {
        Product = product,
        Employee = employee,
        Schedule = schedule,
        AppointmentDate = DateTime.Now,
        UserName = user.Email // Kullanıcının emailini atıyoruz
    };

    return View(viewModel);
}


[HttpPost]
public IActionResult ConfirmAppointment(int productId, int employeeId, int scheduleId, string selectedTime)
{
    var user = _userManager.GetUserAsync(User).Result;
    if (user == null)
    {
        return RedirectToAction("Error");
    }

    // Saat doğrulaması
    if (!TimeSpan.TryParse(selectedTime, out var parsedTime) ||
        parsedTime < TimeSpan.FromHours(9) ||
        parsedTime > TimeSpan.FromHours(17))
    {
        ModelState.AddModelError(string.Empty, "Please select a time between 09:00 and 17:00.");
        return View(new ConfirmAppointmentViewModel
        {
            Product = _serviceManager.ProductService.GetOneProduct(productId, false),
            Employee = _serviceManager.EmployeeService.GetEmployeeById(employeeId, false),
            Schedule = _serviceManager.ScheduleService.GetScheduleById(scheduleId, false)
        });
    }

    // AppointmentDate oluşturma
    var appointmentDate = DateTime.Now.Date.Add(parsedTime); // Güncel tarihe seçilen saati ekle

    // Appointment nesnesini oluştur ve kaydet
    var appointment = new Appointment
    {
        UserId = user.Id,
        EmployeeId = employeeId,
        ProductId = productId,
        ScheduleId = scheduleId,
        AppointmentDate = appointmentDate
    };

    _serviceManager.AppointmentService.CreateAppointment(appointment);

    return RedirectToAction("AppointmentSuccess");
}

[HttpGet]
public IActionResult AppointmentSuccess()
{
    return View();
}

    }
}
