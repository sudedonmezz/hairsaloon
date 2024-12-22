using Services.Contracts;
namespace Services;

public class ServiceManager : IServiceManager
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    private readonly IEmployeeService _employeeService;

    private readonly IScheduleService _scheduleService;

    private readonly IEmployeeScheduleService _employeescheduleService;

    private readonly IEmployeeProductService _employeeProductService;

    public ServiceManager(IProductService productService, ICategoryService categoryService, IEmployeeService employeeService,IScheduleService scheduleService,IEmployeeScheduleService employeescheduleService,IEmployeeProductService employeeProductService)
    {
        _employeeProductService= employeeProductService;
        _employeescheduleService= employeescheduleService;
        _scheduleService= scheduleService;
        _employeeService= employeeService;
        _productService = productService;
        _categoryService = categoryService;
    }

    public IScheduleService ScheduleService => _scheduleService;

    public IProductService ProductService => _productService;

    public ICategoryService CategoryService =>_categoryService;

    public IEmployeeService EmployeeService =>_employeeService;

    public IEmployeeScheduleService EmployeeScheduleService=>_employeescheduleService;

    public IEmployeeProductService EmployeeProductService => _employeeProductService;
}
