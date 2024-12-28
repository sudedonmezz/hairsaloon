using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HairArt.Areas.Employees.Controllers;

[Area("Employees")]
[Authorize(Roles = "Employees")]
public class EmployeesController : Controller
{
    private readonly IServiceManager _serviceManager;

    public EmployeesController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public IActionResult Index()
    {
        var appointments = _serviceManager.AppointmentService.GetAllAppointments(false);
        return View(appointments);
    }
}
