using Entities.Models;
namespace Repositories.Contracts;

public interface IEmployeeScheduleRepository : IRepositoryBase<EmployeeSchedule>
{
    IEnumerable<EmployeeSchedule> GetAllEmployeeSchedules(bool trackChanges);
    EmployeeSchedule? GetEmployeeSchedule(int employeeId, int scheduleId, bool trackChanges);
}
