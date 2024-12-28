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

    // MVC Endpoint'leri
    [HttpGet]
    public IActionResult Index()
    {
        var categories = _serviceManager.CategoryService.GetAllCategories(false);
        return View(categories);
    }

    [HttpGet]
    public IActionResult Edit(int id)
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

        var hasAppointments = _serviceManager.AppointmentService.GetAllAppointments(false)
            .Any(a => a.CategoryId == id);

        if (hasAppointments)
        {
            TempData["ErrorMessage"] = "Cannot delete the category because it has associated appointments.";
            return RedirectToAction("Index");
        }

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

    // REST API Endpoint'leri
    [HttpGet("api/Category/GetAllCategories")]
    public IActionResult GetAllCategoriesApi()
    {
        var categories = _serviceManager.CategoryService.GetAllCategories(false);
        if (categories == null || !categories.Any())
        {
            return NotFound(new { Message = "No categories found." });
        }
        return Ok(categories);
    }

    [HttpDelete("api/Category/DeleteCategory/{id}")]
    public IActionResult DeleteCategoryApi(int id)
    {
        var category = _serviceManager.CategoryService.GetCategoryById(id, false);
        if (category == null)
        {
            return NotFound(new { Message = "Category not found." });
        }

        var hasAppointments = _serviceManager.AppointmentService.GetAllAppointments(false)
            .Any(a => a.CategoryId == id);

        if (hasAppointments)
        {
            return BadRequest(new { Message = "Cannot delete the category because it has associated appointments." });
        }

        if (category.Products != null && category.Products.Any())
        {
            return BadRequest(new { Message = "Cannot delete the category because it has associated products." });
        }

        try
        {
            _serviceManager.CategoryService.DeleteCategory(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting category: {ex.Message}");
            return StatusCode(500, new { Message = $"An error occurred while deleting the category: {ex.Message}" });
        }
    }

    [HttpGet("api/Category/GetCategoryById/{id}")]
public IActionResult GetCategoryByIdApi(int id)
{
    var category = _serviceManager.CategoryService.GetCategoryById(id, false);
    if (category == null)
    {
        return NotFound(new { Message = $"Category with ID {id} not found." });
    }
    return Ok(category);
}

[HttpPut("api/Category/UpdateCategory/{id}")]
public IActionResult UpdateCategoryApi(int id, [FromBody] Category category)
{
    if (id != category.CategoryId)
    {
        return BadRequest(new { Message = "Category ID mismatch." });
    }

    ModelState.Remove("Products");
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    try
    {
        _serviceManager.CategoryService.UpdateCategory(category);
        return Ok(new { Message = "Category updated successfully." });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { Message = $"An error occurred: {ex.Message}" });
    }
}

[HttpPost("api/Category/CreateCategory")]
public IActionResult CreateCategoryApi([FromBody] Category category)
{
    ModelState.Remove("Products");
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    try
    {
        _serviceManager.CategoryService.CreateCategory(category);
        return CreatedAtAction(nameof(GetCategoryByIdApi), new { id = category.CategoryId }, category);
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { Message = $"An error occurred: {ex.Message}" });
    }
}

}
