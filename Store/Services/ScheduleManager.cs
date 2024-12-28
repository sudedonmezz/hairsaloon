using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;


public class ScheduleManager : IScheduleService
{
     private readonly IRepositoryManager _manager;

    public ScheduleManager(IRepositoryManager manager)
    {
        _manager = manager;
    }

public IEnumerable<Schedule> GetAllSchedules(bool trackChanges)
{
    return _manager.Schedule.FindAll(trackChanges).ToList();
}


    public Schedule? GetScheduleById(int scheduleId, bool trackChanges) =>
        _manager.Schedule.GetScheduleById(scheduleId, trackChanges);


        public void DeleteSchedule(int scheduleId)
    {
        var schedule = _manager.Schedule.GetScheduleById(scheduleId, trackChanges: false);
        if (schedule == null)
        {
            throw new Exception($"Schedule with ID {scheduleId} not found.");
        }

        _manager.Schedule.DeleteSchedule(schedule);
        _manager.Save();
    }

        public void CreateSchedule(Schedule schedule)
{
    _manager.Schedule.CreateSchedule(schedule);
    _manager.Save();
}



}
