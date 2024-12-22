using Entities.Models;
namespace Services.Contracts;

public interface IEmployeeProductService
{
    void AssignProductToEmployee(int employeeId, int productId);
    
    IEnumerable<EmployeeProduct> GetProductsByEmployeeId(int employeeId, bool trackChanges);
    IEnumerable<EmployeeProduct> GetEmployeesByProductId(int productId, bool trackChanges);
}
