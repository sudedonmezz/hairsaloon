using Entities.Models;

namespace Repositories.Contracts
{
    public interface IAppointmentRepository
    {
         IEnumerable<Product> GetProductsByCategoryId(int categoryId, bool trackChanges);
        IEnumerable<Appointment> GetAllAppointments(bool trackChanges);
        Appointment? GetAppointmentById(int appointmentId, bool trackChanges);
        void CreateAppointment(Appointment appointment);
        void DeleteAppointment(Appointment appointment);

        public bool HasAppointments(string userId);
         // Kategorisine göre ürünleri getiren yeni metot

         IEnumerable<Appointment> GetAppointmentsByUserId(string userId, bool trackChanges);

         public bool HasAppointmentsForProduct(int productId);

}
    
        
    }

