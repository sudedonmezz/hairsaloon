using Entities.Models;

namespace Repositories.Contracts
{
    public interface IAppointmentRepository
    {
        IEnumerable<Appointment> GetAllAppointments(bool trackChanges);
        Appointment? GetAppointmentById(int appointmentId, bool trackChanges);
        void CreateAppointment(Appointment appointment);
        void DeleteAppointment(Appointment appointment);
    }
}
