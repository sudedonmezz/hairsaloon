using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Entities.Models;
namespace Repositories;

public class RepositoryManager :IRepositoryManager
{
    private readonly RepositoryContext _context;
    private readonly IProductRepository _productRepository;

    private readonly ICategoryRepository _categoryRepository;

    private readonly IEmployeeRepository _employeeRepository;

    private readonly IScheduleRepository _scheduleRepository;

    private readonly IEmployeeProductRepository _employeeProductRepository;

    private readonly IEmployeeScheduleRepository _employeescheduleRepository;

    private readonly IAppointmentRepository _appointmentRepository;
    public RepositoryManager(IProductRepository productRepository,RepositoryContext context,ICategoryRepository categoryRepository,IEmployeeRepository employeeRepository,IScheduleRepository scheduleRepository,IEmployeeScheduleRepository employeescheduleRepository,IEmployeeProductRepository employeeProductRepository,IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository=appointmentRepository;
        _employeeProductRepository=employeeProductRepository;
        _employeescheduleRepository=employeescheduleRepository;
        _scheduleRepository= scheduleRepository;
        _employeeRepository=employeeRepository;
        _categoryRepository=categoryRepository;
        _productRepository = productRepository;
        _context=context;
    }
    public IProductRepository Product => _productRepository;
    public ICategoryRepository Category => _categoryRepository;

    public IScheduleRepository Schedule => _scheduleRepository;
    public IEmployeeRepository Employee => _employeeRepository;

    public IEmployeeScheduleRepository EmployeeSchedule=> _employeescheduleRepository;

      public IEmployeeProductRepository EmployeeProduct=> _employeeProductRepository;

      public IAppointmentRepository Appointment => _appointmentRepository;
    

    public void Save()
    {
        _context.SaveChanges();
    }
}
