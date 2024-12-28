using Entities.Models;
using System.Linq.Expressions;
namespace Repositories.Contracts;

public interface ICategoryRepository : IRepositoryBase<Category>
{
   Category? GetCategoryById(int categoryId, bool trackChanges);

   void UpdateCategory(Category category);

   public void DeleteCategory(Category category);

   public void CreateCategory(Category category);


}
