using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace HairArt.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AppointmentController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public AppointmentController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public IActionResult Index()
        {
            var appointments = _serviceManager.AppointmentService.GetAllAppointments(false);
            return View(appointments);
        }


        [HttpPost]
        public IActionResult DeleteAppointment(int id)
        {
            var appointment = _serviceManager.AppointmentService.GetAppointmentById(id, true);
            if (appointment != null)
            {
                _serviceManager.AppointmentService.DeleteAppointment(appointment);
                TempData["SuccessMessage"] = "Appointment deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Appointment not found.";
            }
            return RedirectToAction("Index");
        }
    }
}
