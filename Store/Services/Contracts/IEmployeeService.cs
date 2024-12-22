using Entities.Models;
namespace Services.Contracts;

public interface IEmployeeService
{
    IEnumerable<Employee> GetEmployeesByCategoryId(int categoryId, bool trackChanges);
    IEnumerable<Employee> GetAllEmployees(bool trackChanges);
    Employee? GetEmployeeById(int employeeId, bool trackChanges);

    IEnumerable<Employee> GetEmployeesByProductId(int productId, bool trackChanges);

}
