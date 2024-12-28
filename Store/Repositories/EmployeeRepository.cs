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

 public IEnumerable<Employee> GetAllEmployeesWithDetails(bool trackChanges)
    {
        return FindAll(trackChanges)
            .Include(e => e.EmployeeProducts)
                .ThenInclude(ep => ep.Product)
            .Include(e => e.EmployeeSchedules)
                .ThenInclude(es => es.Schedule)
            .ToList();
    }


    public Employee GetEmployeeWithProducts(int employeeId, bool trackChanges)
{
    return FindByCondition(e => e.EmployeeId == employeeId, trackChanges)
        .Include(e => e.EmployeeProducts)
            .ThenInclude(ep => ep.Product)
        .SingleOrDefault();
}

public bool HasAppointmentsForEmployee(int employeeId)
{
    return _context.Appointments.Any(a => a.EmployeeId == employeeId);
}

public void DeleteEmployee(int employeeId)
{
    var employee = FindByCondition(e => e.EmployeeId == employeeId, trackChanges: false).FirstOrDefault();
    if (employee != null)
    {
        Remove(employee);
    }

}

public void Create(Employee employee)
{
    _context.Employees.Add(employee);
}

public void UpdateAvailability(int employeeId, bool isAvailable)
{
    var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
    if (employee != null)
    {
        employee.IsAvailable = isAvailable;
        _context.SaveChanges();
    }
}



}