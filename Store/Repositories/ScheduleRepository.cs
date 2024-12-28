using Entities.Models;
using Repositories.Contracts;
namespace Repositories;
public class ScheduleRepository : RepositoryBase<Schedule>, IScheduleRepository
{
    public ScheduleRepository(RepositoryContext context) : base(context)
    {
    }

    public IEnumerable<Schedule> GetAllSchedules(bool trackChanges) =>
        FindAll(trackChanges);

   public Schedule? GetScheduleById(int scheduleId, bool trackChanges)
    {
        return FindByCondition(s => s.ScheduleId == scheduleId, trackChanges)
               .SingleOrDefault(); // IQueryable -> Schedule dönüşümü
    }

    public void DeleteSchedule(Schedule schedule)
{
    Remove(schedule);
}

public void CreateSchedule(Schedule schedule)
{
    Create(schedule);
}



}
