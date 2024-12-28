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

        public IEnumerable<EmployeeSchedule> GetSchedulesByEmployee(int employeeId, bool trackChanges) =>
        _manager.EmployeeSchedule.GetSchedulesByEmployee(employeeId, trackChanges);
     

        public bool IsScheduleAssigned(int scheduleId)
{
    return _manager.EmployeeSchedule
        .FindByCondition(es => es.ScheduleId == scheduleId, trackChanges: false)
        .Any();
}


 public void RemoveEmployeeSchedules(int employeeId)
    {
        // Belirtilen employeeId'ye sahip tüm EmployeeSchedule kayıtlarını getir
        var employeeSchedules = _manager.EmployeeSchedule.GetSchedulesByEmployee(employeeId, trackChanges: false);

        // Tüm kayıtları sil
        foreach (var schedule in employeeSchedules)
        {
            _manager.EmployeeSchedule.Remove(schedule);
        }

        // Değişiklikleri kaydet
        _manager.Save();
    }

   public void UpdateEmployeeSchedules(int employeeId, List<int> scheduleIds)
{
    var existingSchedules = _manager.EmployeeSchedule
        .GetSchedulesByEmployee(employeeId, trackChanges: true)
        .ToList();

    var schedulesToRemove = existingSchedules
        .Where(es => !scheduleIds.Contains(es.ScheduleId))
        .ToList();

    foreach (var schedule in schedulesToRemove)
    {
        _manager.EmployeeSchedule.Remove(schedule);
    }

    var schedulesToAdd = scheduleIds
        .Where(sid => !existingSchedules.Any(es => es.ScheduleId == sid))
        .Select(sid => new EmployeeSchedule { EmployeeId = employeeId, ScheduleId = sid })
        .ToList();

    foreach (var schedule in schedulesToAdd)
    {
        _manager.EmployeeSchedule.Create(schedule);
    }

    _manager.Save();
}



}
