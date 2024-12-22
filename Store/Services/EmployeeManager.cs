using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
namespace Services;

public class EmployeeManager : IEmployeeService
{
    private readonly IRepositoryManager _manager;

    public EmployeeManager(IRepositoryManager manager)
    {
        _manager = manager;
    }
    public IEnumerable<Employee> GetEmployeesByCategoryId(int categoryId, bool trackChanges) =>
        _manager.Employee.GetEmployeesByCategoryId(categoryId, trackChanges);

    public IEnumerable<Employee> GetAllEmployees(bool trackChanges) =>
        _manager.Employee.GetAllEmployees(trackChanges);

    public Employee? GetEmployeeById(int employeeId, bool trackChanges) =>
        _manager.Employee.GetEmployeeById(employeeId, trackChanges);

        public IEnumerable<Employee> GetEmployeesByProductId(int productId, bool trackChanges) =>
    _manager.Employee.GetEmployeesByProductId(productId, trackChanges);


}
