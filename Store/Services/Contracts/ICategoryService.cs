using Entities.Models;
namespace Services.Contracts;

public interface ICategoryService
{
    IEnumerable<Category> GetAllCategories(bool trackChanges);

     Category? GetCategoryById(int categoryId, bool trackChanges);

     void UpdateCategory(Category category);

     public void DeleteCategory(int categoryId);

     public void CreateCategory(Category category);

}
