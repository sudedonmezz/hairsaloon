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

        public IEnumerable<Product> GetProductsByCategoryId(int categoryId, bool trackChanges)
        {
            return _context.Categories
                .Where(c => c.CategoryId == categoryId)
                .SelectMany(c => c.Products)
                .ToList();
        }
    
       public IEnumerable<Appointment> GetAllAppointments(bool trackChanges) =>
    FindAll(trackChanges)
         .Include(a => a.User) // Kullanıcı bilgisi
        .Include(a => a.EmployeeProduct) // Çalışan-Ürün ilişkisi
            .ThenInclude(ep => ep.Product) // Ürün bilgisi
        .Include(a => a.EmployeeProduct) // Çalışan-Ürün ilişkisi
            .ThenInclude(ep => ep.Employee) // Çalışan bilgisi
        .Include(a => a.EmployeeSchedule) // Çalışan-Takvim ilişkisi
            .ThenInclude(es => es.Schedule) // Takvim bilgisi
        .Include(a => a.Category) // Kategori bilgisi
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
                .Include(a => a.Category)
            .SingleOrDefault();


        public void CreateAppointment(Appointment appointment) => Create(appointment);

        public void DeleteAppointment(Appointment appointment) => Remove(appointment);

        public bool HasAppointments(string userId)
        {
    return FindByCondition(a => a.UserId == userId, false).Any();
        }

    public IEnumerable<Appointment> GetAppointmentsByUserId(string userId, bool trackChanges) =>
        FindByCondition(a => a.UserId == userId, trackChanges).ToList();


        public bool HasAppointmentsForProduct(int productId) =>
        FindByCondition(a => a.ProductId == productId, trackChanges: false).Any();



public IEnumerable<Appointment> GetAppointmentsByCategoryId(int categoryId, bool trackChanges)
{
    return FindByCondition(a => a.CategoryId == categoryId, trackChanges).ToList();
}



    }
}
