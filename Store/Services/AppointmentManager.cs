using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class AppointmentManager : IAppointmentService
    {
        private readonly IRepositoryManager _repository;

        public AppointmentManager(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public IEnumerable<Appointment> GetAllAppointments(bool trackChanges) =>
            _repository.Appointment.GetAllAppointments(trackChanges);

        public Appointment? GetAppointmentById(int appointmentId, bool trackChanges) =>
            _repository.Appointment.GetAppointmentById(appointmentId, trackChanges);

    public void CreateAppointment(Appointment appointment)
        {
            if (appointment == null)
                throw new ArgumentNullException(nameof(appointment));

            // Repository'deki Create fonksiyonu çağrılır
            _repository.Appointment.CreateAppointment(appointment);
            _repository.Save(); // Veritabanına değişiklikleri kaydeder
        }

        public void DeleteAppointment(Appointment appointment) =>
            _repository.Appointment.DeleteAppointment(appointment);
    }
}
