using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Entities.Models;

namespace HairArt.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CategoryController : Controller
{
    private readonly IServiceManager _serviceManager;

    public CategoryController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var categories = _serviceManager.CategoryService.GetAllCategories(false);
        return View(categories);
    }



    [HttpGet]
public IActionResult Edit(int id)
{
    // Kategoriyi ID'ye göre getir
    var category = _serviceManager.CategoryService.GetCategoryById(id, false);
    if (category == null)
    {
        TempData["ErrorMessage"] = "Category not found.";
        return RedirectToAction("Index");
    }

    return View(category);
}


[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Edit([Bind("CategoryId,CategoryName,CategoryDescription,ImageUrl")] Category category)
{
    ModelState.Remove("Products");
    if (!ModelState.IsValid)
    {
        TempData["ErrorMessage"] = "Please correct the errors.";
        return View(category);
    }

    try
    {
        // Products alanı güncellenmeden Category nesnesini gönder
        _serviceManager.CategoryService.UpdateCategory(category);

        TempData["SuccessMessage"] = "Category updated successfully.";
        return RedirectToAction("Index");
    }
    catch (Exception ex)
    {
        TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
        return View(category);
    }
}

[HttpGet]
public IActionResult Delete(int id)
{
    var category = _serviceManager.CategoryService.GetCategoryById(id, false);
    if (category == null)
    {
        TempData["ErrorMessage"] = "Category not found.";
        return RedirectToAction("Index");
    }

    return View(category);
}

[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult DeleteConfirmed(int id)
{
    var category = _serviceManager.CategoryService.GetCategoryById(id, false);
    if (category == null)
    {
        TempData["ErrorMessage"] = "Category not found.";
        return RedirectToAction("Index");
    }

    // Appointment kontrolü
    var hasAppointments = _serviceManager.AppointmentService.GetAllAppointments(false)
        .Any(a => a.CategoryId == id);

    if (hasAppointments)
    {
        TempData["ErrorMessage"] = "Cannot delete the category because it has associated appointments.";
        return RedirectToAction("Index");
    }

    // Product kontrolü
    if (category.Products != null && category.Products.Any())
    {
        TempData["ErrorMessage"] = "Cannot delete the category because it has associated products.";
        return RedirectToAction("Index");
    }

    try
    {
        _serviceManager.CategoryService.DeleteCategory(id);
        TempData["SuccessMessage"] = "Category deleted successfully.";
    }
    catch (Exception ex)
    {
        TempData["ErrorMessage"] = $"An error occurred while deleting the category: {ex.Message}";
        return RedirectToAction("Index");
    }

    return RedirectToAction("Index"); // Tüm kod yollarında bir return olmalı
}

[HttpGet]
public IActionResult Create()
{
    return View();
}

[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(Category category)
{
    ModelState.Remove("Products");
    if (!ModelState.IsValid)
    {
        TempData["ErrorMessage"] = "Please correct the errors.";
        return View(category);
    }

    try
    {
        _serviceManager.CategoryService.CreateCategory(category);
        TempData["SuccessMessage"] = "Category created successfully.";
        return RedirectToAction("Index");
    }
    catch (Exception ex)
    {
        TempData["ErrorMessage"] = $"An error occurred while creating the category: {ex.Message}";
        return View(category);
    }
}



}
