using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
namespace Services;

public class CategoryManager : ICategoryService
{
    private readonly IRepositoryManager _manager;

    public CategoryManager(IRepositoryManager manager)
    {
        _manager = manager;
    }
    public IEnumerable<Category> GetAllCategories(bool trackChanges)
    {
        return _manager.Category.FindAll(trackChanges);
    }

    public Category? GetCategoryById(int categoryId, bool trackChanges)
    {
        return _manager.Category.FindByCondition(c => c.CategoryId == categoryId, trackChanges)
                                                    .SingleOrDefault();
    }

public void UpdateCategory(Category category)
{
    _manager.Category.UpdateCategory(category);
}

public void DeleteCategory(int categoryId)
{
    var category = _manager.Category.GetCategoryById(categoryId, false);
    if (category == null)
    {
        throw new Exception("Category not found.");
    }

    _manager.Category.DeleteCategory(category);
}

public void CreateCategory(Category category)
{
    if (category == null)
    {
        throw new ArgumentNullException(nameof(category));
    }

    _manager.Category.CreateCategory(category);
    _manager.Save();
}




}
