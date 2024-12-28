using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Entities.Models;

namespace HairArt.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ScheduleController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public ScheduleController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var schedules = _serviceManager.ScheduleService.GetAllSchedules(false);
            return View(schedules);
        }



        [HttpGet]
public IActionResult Delete([FromRoute(Name = "id")] int id)
{
    // Schedule'in herhangi bir EmployeeSchedule ile eşleşip eşleşmediğini kontrol et
    bool isAssigned = _serviceManager.EmployeeScheduleService.IsScheduleAssigned(id);

    if (isAssigned)
    {
        // TempData ile kullanıcıya mesaj ilet
        TempData["ErrorMessage"] = "Bu çalışma saati bir çalışana atanmıştır ve silinemez.";
        return RedirectToAction("Index");
    }

    // Eğer eşleşme yoksa silme işlemini gerçekleştir
    _serviceManager.ScheduleService.DeleteSchedule(id);
    TempData["SuccessMessage"] = "Çalışma saati başarıyla silindi.";
    return RedirectToAction("Index");
}


   [HttpGet]
public IActionResult Create()
{
    return View(); // Boş bir form görüntüler
}

[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create([FromForm] Schedule schedule)
{
    if (ModelState.IsValid)
    {
        _serviceManager.ScheduleService.CreateSchedule(schedule);
        TempData["SuccessMessage"] = "Takvim başarıyla eklendi.";
        return RedirectToAction("Index");
    }

    TempData["ErrorMessage"] = "Takvim ekleme sırasında bir hata oluştu.";
    return View(schedule);
}



    }




}
