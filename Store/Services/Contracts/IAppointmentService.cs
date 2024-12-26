using Entities.Models;

namespace Services.Contracts
{
    public interface IAppointmentService
    {
        IEnumerable<Appointment> GetAllAppointments(bool trackChanges);
        Appointment? GetAppointmentById(int appointmentId, bool trackChanges);
        void CreateAppointment(Appointment appointment);
        void DeleteAppointment(Appointment appointment);
    }
}
