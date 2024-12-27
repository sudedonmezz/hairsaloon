using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;
using Entities.Models;
namespace Repositories;

public class RepositoryContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    public DbSet<Employee> Employees { get; set; }
public DbSet<Schedule> Schedules { get; set; }
public DbSet<EmployeeSchedule> EmployeeSchedules { get; set; }

public DbSet<EmployeeProduct> EmployeeProducts { get; set; }

public DbSet<ApplicationUser> ApplicationUsers { get; set; }

public DbSet<Appointment> Appointments { get; set; }


    public RepositoryContext(DbContextOptions<RepositoryContext> options):base(options) //veritabanı tanımlanmasına şart
    {

    }

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // EmployeeProduct için birincil anahtar
    modelBuilder.Entity<EmployeeProduct>()
        .HasKey(ep => new { ep.EmployeeId, ep.ProductId });



      modelBuilder.Entity<Appointment>()
    .HasOne(a => a.Category)
    .WithMany(c => c.Appointments)
    .HasForeignKey(a => a.CategoryId)
    .OnDelete(DeleteBehavior.Restrict);



    // Appointment ile EmployeeProduct ilişkisi
    modelBuilder.Entity<Appointment>()
        .HasOne(a => a.EmployeeProduct)
        .WithMany(ep => ep.Appointments)
        .HasForeignKey(a => new { a.EmployeeId, a.ProductId })
        .OnDelete(DeleteBehavior.Restrict);

    // EmployeeSchedule için bileşik anahtar
    modelBuilder.Entity<EmployeeSchedule>()
        .HasKey(es => new { es.EmployeeId, es.ScheduleId });

        modelBuilder.Entity<EmployeeSchedule>()
    .HasOne(es => es.Schedule)
    .WithMany(s => s.EmployeeSchedules)
    .HasForeignKey(es => es.ScheduleId)
    .OnDelete(DeleteBehavior.Restrict);

    // Appointment ile EmployeeSchedule ilişkisi
    modelBuilder.Entity<Appointment>()
        .HasOne(a => a.EmployeeSchedule)
        .WithMany(es => es.Appointments)
        .HasForeignKey(a => new { a.EmployeeId, a.ScheduleId })
        .OnDelete(DeleteBehavior.Restrict);

    // Kullanıcı ile Appointment ilişkisi
    modelBuilder.Entity<Appointment>()
        .HasOne(a => a.User)
        .WithMany(u => u.Appointments)
        .HasForeignKey(a => a.UserId)
        .OnDelete(DeleteBehavior.Restrict);

    base.OnModelCreating(modelBuilder);
}



}
