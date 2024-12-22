using Entities.Models;
namespace Services.Contracts;
public interface IScheduleService
{
     IEnumerable<Schedule> GetAllSchedules(bool trackChanges);
    Schedule? GetScheduleById(int scheduleId, bool trackChanges);
}
