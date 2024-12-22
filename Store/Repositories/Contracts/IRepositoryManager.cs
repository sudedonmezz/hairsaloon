namespace Repositories.Contracts;

public interface IRepositoryManager
{
    IProductRepository Product { get; }
    ICategoryRepository Category { get; }

    IEmployeeRepository Employee { get; }

    IScheduleRepository Schedule { get; }

    IEmployeeScheduleRepository EmployeeSchedule { get; }

    IEmployeeProductRepository EmployeeProduct { get; }

    void Save();
}
