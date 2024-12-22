using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
namespace Services;

public class EmployeeProductManager : IEmployeeProductService
{
    private readonly IRepositoryManager _manager;

    public EmployeeProductManager(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public void AssignProductToEmployee(int employeeId, int productId)
    {
        var employeeProduct = new EmployeeProduct
        {
            EmployeeId = employeeId,
            ProductId = productId
        };

        _manager.EmployeeProduct.CreateEmployeeProduct(employeeProduct);
        _manager.Save();
    }

   
    

    public IEnumerable<EmployeeProduct> GetProductsByEmployeeId(int employeeId, bool trackChanges) =>
        _manager.EmployeeProduct.GetProductsByEmployeeId(employeeId, trackChanges);

    public IEnumerable<EmployeeProduct> GetEmployeesByProductId(int productId, bool trackChanges) =>
        _manager.EmployeeProduct.GetEmployeesByProductId(productId, trackChanges);

}
