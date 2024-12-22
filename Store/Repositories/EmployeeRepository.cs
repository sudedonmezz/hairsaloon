using Entities.Models;
using Repositories.Contracts;
using Microsoft.EntityFrameworkCore;


namespace Repositories;

public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
{
    public EmployeeRepository(RepositoryContext context) : base(context)
    {
    }

  public IEnumerable<Employee> GetAllEmployees(bool trackChanges) =>
        FindAll(trackChanges).ToList();

    public Employee? GetEmployeeById(int employeeId, bool trackChanges) =>
        FindByCondition(e => e.EmployeeId == employeeId, trackChanges).SingleOrDefault();

   public IEnumerable<Employee> GetEmployeesByCategoryId(int categoryId, bool trackChanges)
{
    return _context.EmployeeProducts
        .Include(ep => ep.Product)
        .Where(ep => ep.Product.CategoryId == categoryId)
        .Select(ep => ep.Employee)
        .Distinct()
        .ToList();
}

public IEnumerable<Employee> GetEmployeesByProductId(int productId, bool trackChanges)
{
    return _context.EmployeeProducts
        .Include(ep => ep.Employee)
        .Where(ep => ep.ProductId == productId)
        .Select(ep => ep.Employee)
        .Distinct()
        .ToList();
}

}