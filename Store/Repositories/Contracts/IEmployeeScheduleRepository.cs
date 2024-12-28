using Entities.Models;
namespace Repositories.Contracts;

public interface IEmployeeScheduleRepository : IRepositoryBase<EmployeeSchedule>
{
    IEnumerable<EmployeeSchedule> GetAllEmployeeSchedules(bool trackChanges);
    EmployeeSchedule? GetEmployeeSchedule(int employeeId, int scheduleId, bool trackChanges);
    IEnumerable<EmployeeSchedule> GetSchedulesByEmployee(int employeeId, bool trackChanges); // Yeni Metod


    public bool IsScheduleAssigned(int scheduleId);

   void UpdateEmployeeSchedules(int employeeId, List<int> scheduleIds);

   public void RemoveRange(IEnumerable<EmployeeSchedule> schedules);

   public void AddRange(IEnumerable<EmployeeSchedule> schedules);
}
