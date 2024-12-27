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

   
    
 public IEnumerable<EmployeeProduct> GetProductsByEmployeeId(int employeeId, bool trackChanges)
    {
        return _manager.EmployeeProduct.GetProductsByEmployeeId(employeeId, trackChanges);
    }

    public IEnumerable<EmployeeProduct> GetEmployeesByProductId(int productId, bool trackChanges)
    {
        return _manager.EmployeeProduct.GetEmployeesByProductId(productId, trackChanges);
    }


    public void UpdateEmployeeProducts(int employeeId, List<int> productIds)
    {
        var existingProducts = _manager.EmployeeProduct.GetProductsByEmployeeId(employeeId, true).ToList();

        // Silinmesi gereken ürünler
        var toRemove = existingProducts.Where(ep => !productIds.Contains(ep.ProductId)).ToList();
        foreach (var ep in toRemove)
        {
            _manager.EmployeeProduct.Remove(ep);
        }

        // Eklenmesi gereken ürünler
        var toAdd = productIds.Where(pid => !existingProducts.Any(ep => ep.ProductId == pid))
                              .Select(pid => new EmployeeProduct { EmployeeId = employeeId, ProductId = pid })
                              .ToList();

        foreach (var ep in toAdd)
        {
            _manager.EmployeeProduct.Create(ep);
        }

        _manager.Save();
    }

}
