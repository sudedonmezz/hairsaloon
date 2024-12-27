using Entities.Models;
using Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class EmployeeProductRepository : RepositoryBase<EmployeeProduct>, IEmployeeProductRepository
{
     public EmployeeProductRepository(RepositoryContext context) : base(context)
    {
        
    }

    public void CreateEmployeeProduct(EmployeeProduct employeeProduct) => Create(employeeProduct);

   

    public IEnumerable<EmployeeProduct> GetAllEmployeeProducts(bool trackChanges) =>
        FindAll(trackChanges).ToList();
public IEnumerable<EmployeeProduct> GetProductsByEmployeeId(int employeeId, bool trackChanges)
{
    return FindByCondition(ep => ep.EmployeeId == employeeId, trackChanges)
        .Include(ep => ep.Product)
        .ToList();
}

public IEnumerable<EmployeeProduct> GetEmployeesByProductId(int productId, bool trackChanges)
{
    return FindByCondition(ep => ep.ProductId == productId, trackChanges)
        .Include(ep => ep.Employee)
        .ToList();
}



}
