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

         public IEnumerable<Product> GetProductsByCategoryId(int categoryId, bool trackChanges)
        {
            return _repository.Appointment.GetProductsByCategoryId(categoryId, trackChanges);
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


            public bool UserHasAppointments(string userId)
            {
    return _repository.Appointment.HasAppointments(userId);
            }

            public IEnumerable<Appointment> GetAppointmentsByUserId(string userId, bool trackChanges)
    {
        return _repository.Appointment.GetAppointmentsByUserId(userId, trackChanges);
    }

   public bool CategoryHasAppointments(int categoryId)
{
    return _repository.Appointment.GetAllAppointments(false)
        .Any(a => a.CategoryId == categoryId);
}

public IEnumerable<ApplicationUser> GetUsersByEmployeeId(int employeeId)
    {
        return _repository.Appointment.GetUsersByEmployeeId(employeeId);
    }

    public void ApproveAppointment(int appointmentId)
{
    var appointment = _repository.Appointment.GetAppointmentById(appointmentId, trackChanges: true);
    if (appointment == null)
    {
        throw new ArgumentException($"Appointment with id {appointmentId} not found.");
    }

    appointment.IsApproved = true;
    _repository.Save();
}



    }
}
