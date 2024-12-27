using Entities.Models;
using Repositories.Contracts;
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
}
