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

public IEnumerable<Schedule> GetAllSchedules(bool trackChanges) =>
        _manager.Schedule.GetAllSchedules(trackChanges);

    public Schedule? GetScheduleById(int scheduleId, bool trackChanges) =>
        _manager.Schedule.GetScheduleById(scheduleId, trackChanges);

}
