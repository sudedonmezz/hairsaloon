using Entities.Models;
using Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class AppointmentRepository : RepositoryBase<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(RepositoryContext context) : base(context)
        {
        }

       public IEnumerable<Appointment> GetAllAppointments(bool trackChanges) =>
    FindAll(trackChanges)
        .Include(a => a.User)
        .Include(a => a.EmployeeProduct)
        .ThenInclude(ep => ep.Product)
        .Include(a => a.EmployeeSchedule)
        .ThenInclude(es => es.Schedule)
        .ToList();

        public Appointment? GetAppointmentById(int appointmentId, bool trackChanges) =>
            FindByCondition(a => a.AppointmentId == appointmentId, trackChanges)
            .Include(a => a.User)
            .Include(a => a.EmployeeProduct)
                .ThenInclude(ep => ep.Employee)
            .Include(a => a.EmployeeProduct)
                .ThenInclude(ep => ep.Product)
            .Include(a => a.EmployeeSchedule)
                .ThenInclude(es => es.Schedule)
            .SingleOrDefault();

        public void CreateAppointment(Appointment appointment) => Create(appointment);

        public void DeleteAppointment(Appointment appointment) => Remove(appointment);
    }
}
