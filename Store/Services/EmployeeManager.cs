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

    public IEnumerable<Employee> GetAllEmployeesWithDetails(bool trackChanges) => _manager.Employee.GetAllEmployeesWithDetails(trackChanges);



    public Employee GetEmployeeWithProducts(int employeeId, bool trackChanges)
{
    return _manager.Employee.GetEmployeeWithProducts(employeeId, trackChanges);
}

public void UpdateEmployeeProducts(int employeeId, List<int> productIds)
{
    var employee = _manager.Employee.GetEmployeeWithProducts(employeeId, trackChanges: true);

    if (employee == null)
    {
        throw new Exception("Çalışan bulunamadı.");
    }

    // Mevcut ürünleri temizle
    employee.EmployeeProducts.Clear();

    // Yeni ürünleri ekle
    foreach (var productId in productIds)
    {
        employee.EmployeeProducts.Add(new EmployeeProduct
        {
            EmployeeId = employeeId,
            ProductId = productId
        });
    }

    _manager.Save();
}

public bool HasAppointmentsForEmployee(int employeeId)
{
    return _manager.Employee.HasAppointmentsForEmployee(employeeId);
}

public void DeleteEmployee(int employeeId)
{
    var employee = _manager.Employee.GetEmployeeById(employeeId, trackChanges: false);
    if (employee == null)
    {
        throw new Exception("Çalışan bulunamadı.");
    }

    _manager.Employee.Remove(employee);
    _manager.Save();
}

public void CreateEmployee(Employee employee)
{
    _manager.Employee.Create(employee);
    _manager.Save();
}

public void UpdateAvailability(int employeeId, bool isAvailable)
{
    _manager.Employee.UpdateAvailability(employeeId, isAvailable);
    _manager.Save();
}



}
