using Entities.Models;
namespace Services.Contracts;

public interface IEmployeeScheduleService
{
    IEnumerable<EmployeeSchedule> GetAllEmployeeSchedules(bool trackChanges);
    EmployeeSchedule? GetEmployeeSchedule(int employeeId, int scheduleId, bool trackChanges);

     IEnumerable<EmployeeSchedule> GetSchedulesByEmployee(int employeeId, bool trackChanges);
     
      public bool IsScheduleAssigned(int scheduleId);

    void UpdateEmployeeSchedules(int employeeId, List<int> scheduleIds);
    void RemoveEmployeeSchedules(int employeeId);
   
}
