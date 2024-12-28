using Entities.Models;

namespace Services.Contracts
{
    public interface IAppointmentService
    {

        IEnumerable<Product> GetProductsByCategoryId(int categoryId, bool trackChanges);
        IEnumerable<Appointment> GetAllAppointments(bool trackChanges);
        Appointment? GetAppointmentById(int appointmentId, bool trackChanges);
        void CreateAppointment(Appointment appointment);
        void DeleteAppointment(Appointment appointment);

        public bool UserHasAppointments(string userId);

        // Yeni yöntem
    IEnumerable<Appointment> GetAppointmentsByUserId(string userId, bool trackChanges);


    public bool CategoryHasAppointments(int categoryId);
    }
}
