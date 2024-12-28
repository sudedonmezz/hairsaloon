using Entities.Models;
namespace Services.Contracts;

public interface IEmployeeService
{
    IEnumerable<Employee> GetEmployeesByCategoryId(int categoryId, bool trackChanges);
    IEnumerable<Employee> GetAllEmployees(bool trackChanges);
    Employee? GetEmployeeById(int employeeId, bool trackChanges);

    IEnumerable<Employee> GetEmployeesByProductId(int productId, bool trackChanges);

    IEnumerable<Employee> GetAllEmployeesWithDetails(bool trackChanges);

    void UpdateEmployeeProducts(int employeeId, List<int> productIds);
    Employee GetEmployeeWithProducts(int employeeId, bool trackChanges);

    public void DeleteEmployee(int employeeId);

    public bool HasAppointmentsForEmployee(int employeeId);

    void CreateEmployee(Employee employee);




}
