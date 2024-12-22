namespace Services.Contracts;

public interface IServiceManager
{
    IProductService ProductService { get; }
    ICategoryService CategoryService { get; }

    IEmployeeService EmployeeService { get; }

    IScheduleService ScheduleService { get; }

    IEmployeeScheduleService EmployeeScheduleService { get; }

    IEmployeeProductService EmployeeProductService { get; }
}
