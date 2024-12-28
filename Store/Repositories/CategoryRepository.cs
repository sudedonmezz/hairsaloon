using Entities.Models;
using Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(RepositoryContext context) : base(context)
    {
        
    }

     public Category? GetCategoryById(int categoryId, bool trackChanges)
    {
        return FindByCondition(c => c.CategoryId == categoryId, trackChanges).SingleOrDefault();
    }

public void UpdateCategory(Category category)
    {
        var existingCategory = FindByCondition(c => c.CategoryId == category.CategoryId, trackChanges: true).SingleOrDefault();
        if (existingCategory == null)
        {
            throw new Exception("Category not found.");
        }

        existingCategory.CategoryName = category.CategoryName;
        existingCategory.CategoryDescription = category.CategoryDescription;
        existingCategory.ImageUrl = category.ImageUrl;

        // Save changes after updating the entity
        _context.SaveChanges();
    }


 public void DeleteCategory(Category category)
{
    try
    {
        _context.Categories.Remove(category);
        _context.SaveChanges();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error during SaveChanges: {ex.Message}");
        if (ex.InnerException != null)
        {
            Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
        }
        throw;
    }
}

public void CreateCategory(Category category)
{
    Create(category); // RepositoryBase'deki Create metodunu kullanır
}




}
