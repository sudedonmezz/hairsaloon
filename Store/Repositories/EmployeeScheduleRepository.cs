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

public bool IsScheduleAssigned(int scheduleId)
{
    return FindByCondition(es => es.ScheduleId == scheduleId, trackChanges: false).Any();
}

public void UpdateEmployeeSchedules(int employeeId, List<int> scheduleIds)
    {
        var existingSchedules = FindByCondition(es => es.EmployeeId == employeeId, trackChanges: true).ToList();

        var schedulesToRemove = existingSchedules.Where(es => !scheduleIds.Contains(es.ScheduleId)).ToList();
        var schedulesToAdd = scheduleIds.Where(sid => !existingSchedules.Any(es => es.ScheduleId == sid))
                                        .Select(sid => new EmployeeSchedule { EmployeeId = employeeId, ScheduleId = sid })
                                        .ToList();

        foreach (var schedule in schedulesToRemove)
        {
            _context.Set<EmployeeSchedule>().Remove(schedule);
        }

        foreach (var schedule in schedulesToAdd)
        {
            _context.Set<EmployeeSchedule>().Add(schedule);
        }

        _context.SaveChanges();
    }

public void RemoveRange(IEnumerable<EmployeeSchedule> schedules)
{
    foreach (var schedule in schedules)
    {
        _context.Set<EmployeeSchedule>().Remove(schedule);
    }
}

public void AddRange(IEnumerable<EmployeeSchedule> schedules)
{
    foreach (var schedule in schedules)
    {
        _context.Set<EmployeeSchedule>().Add(schedule);
    }
}


}
