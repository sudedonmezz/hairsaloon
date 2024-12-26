using Entities.Models;
using Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class EmployeeScheduleRepository : RepositoryBase<EmployeeSchedule>, IEmployeeScheduleRepository
{
    public EmployeeScheduleRepository(RepositoryContext context) : base(context)
    {
    }

    
    public EmployeeSchedule? GetEmployeeSchedule(int employeeId, int scheduleId, bool trackChanges)
    {
       return FindByCondition(es => es.EmployeeId == employeeId && es.ScheduleId == scheduleId, trackChanges)
              .SingleOrDefault();
        
    }

    public IEnumerable<EmployeeSchedule> GetSchedulesByEmployee(int employeeId, bool trackChanges)
    {
        return FindByCondition(es => es.EmployeeId == employeeId, trackChanges)
            .Include(es => es.Schedule) // Schedule ilişkisini dahil ediyoruz
            .ToList();
    }

    public IEnumerable<EmployeeSchedule> GetAllEmployeeSchedules(bool trackChanges)
{
    return FindAll(trackChanges)
        .Include(es => es.Employee) // Employee ilişkisini yükle
        .Include(es => es.Schedule) // Schedule ilişkisini yükle
        .ToList();
}

}
