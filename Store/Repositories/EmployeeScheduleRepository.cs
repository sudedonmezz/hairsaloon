using Entities.Models;
using Repositories.Contracts;
namespace Repositories;
public class EmployeeScheduleRepository : RepositoryBase<EmployeeSchedule>, IEmployeeScheduleRepository
{
    public EmployeeScheduleRepository(RepositoryContext context) : base(context)
    {
    }

    public IEnumerable<EmployeeSchedule> GetAllEmployeeSchedules(bool trackChanges) =>
        FindAll(trackChanges);

   public EmployeeSchedule? GetEmployeeSchedule(int employeeId, int scheduleId, bool trackChanges)
    {
        return FindByCondition(es => es.EmployeeId == employeeId && es.ScheduleId == scheduleId, trackChanges)
               .SingleOrDefault(); // IQueryable -> EmployeeSchedule dönüşümü
    }

}
