using Entities.Models;
namespace Repositories.Contracts;

public interface IEmployeeProductRepository : IRepositoryBase<EmployeeProduct>
{
    void CreateEmployeeProduct(EmployeeProduct employeeProduct);
    
    IEnumerable<EmployeeProduct> GetAllEmployeeProducts(bool trackChanges);
    IEnumerable<EmployeeProduct> GetProductsByEmployeeId(int employeeId, bool trackChanges);
    IEnumerable<EmployeeProduct> GetEmployeesByProductId(int productId, bool trackChanges);
}
