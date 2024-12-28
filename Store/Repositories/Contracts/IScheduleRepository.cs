using Entities.Models;
namespace Repositories.Contracts;
public interface IScheduleRepository : IRepositoryBase<Schedule>
{
    IEnumerable<Schedule> GetAllSchedules(bool trackChanges);
    Schedule? GetScheduleById(int scheduleId, bool trackChanges);

    public void DeleteSchedule(Schedule schedule);

    void CreateSchedule(Schedule schedule);

}
