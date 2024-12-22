using Entities.Models;
using Repositories.Contracts;
namespace Repositories;

public class EmployeeProductRepository : RepositoryBase<EmployeeProduct>, IEmployeeProductRepository
{
     public EmployeeProductRepository(RepositoryContext context) : base(context)
    {
        
    }

    public void CreateEmployeeProduct(EmployeeProduct employeeProduct) => Create(employeeProduct);

   

    public IEnumerable<EmployeeProduct> GetAllEmployeeProducts(bool trackChanges) =>
        FindAll(trackChanges).ToList();

    public IEnumerable<EmployeeProduct> GetProductsByEmployeeId(int employeeId, bool trackChanges) =>
        FindByCondition(ep => ep.EmployeeId == employeeId, trackChanges).ToList();

    public IEnumerable<EmployeeProduct> GetEmployeesByProductId(int productId, bool trackChanges) =>
        FindByCondition(ep => ep.ProductId == productId, trackChanges).ToList();
}
