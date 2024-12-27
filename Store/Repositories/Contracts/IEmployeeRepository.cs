using Entities.Models;
using System.Linq; // Bu satır eklendi
using System.Collections.Generic;
namespace Repositories.Contracts;

public interface IEmployeeRepository : IRepositoryBase<Employee>
{
    IEnumerable<Employee> GetEmployeesByCategoryId(int categoryId, bool trackChanges);
     IEnumerable<Employee> GetAllEmployees(bool trackChanges);
    Employee? GetEmployeeById(int employeeId, bool trackChanges);

    IEnumerable<Employee> GetEmployeesByProductId(int productId, bool trackChanges);

    IEnumerable<Employee> GetAllEmployeesWithDetails(bool trackChanges);

    Employee GetEmployeeWithProducts(int employeeId, bool trackChanges);

    public bool HasAppointmentsForEmployee(int employeeId);

    public void DeleteEmployee(int employeeId);

}
