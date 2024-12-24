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

    public RepositoryContext(DbContextOptions<RepositoryContext> options):base(options) //veritabanı tanımlanmasına şart
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // EmployeeProduct için birincil anahtar
    modelBuilder.Entity<EmployeeProduct>()
        .HasKey(ep => new { ep.EmployeeId, ep.ProductId });


        // EmployeeSchedule için bileşik anahtar tanımlayın
    modelBuilder.Entity<EmployeeSchedule>()
        .HasKey(es => new { es.EmployeeId, es.ScheduleId });
        base.OnModelCreating(modelBuilder);
      
       // modelBuilder.ApplyConfiguration(new ProductConfig());
       // modelBuilder.ApplyConfiguration(new CategoryConfig());

       //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
    }
}
