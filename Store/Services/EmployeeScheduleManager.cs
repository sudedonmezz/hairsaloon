using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
namespace Services;

public class EmployeeScheduleManager : IEmployeeScheduleService
{
    private readonly IRepositoryManager _manager;

    public EmployeeScheduleManager(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public IEnumerable<EmployeeSchedule> GetAllEmployeeSchedules(bool trackChanges) =>
        _manager.EmployeeSchedule.GetAllEmployeeSchedules(trackChanges);
 
    public EmployeeSchedule? GetEmployeeSchedule(int employeeId, int scheduleId, bool trackChanges) =>
        _manager.EmployeeSchedule.GetEmployeeSchedule(employeeId, scheduleId, trackChanges);
}
