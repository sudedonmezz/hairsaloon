using HairArt.Models;
using Repositories;
using Repositories.Contracts;
using Services;
using Services.Contracts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<RepositoryContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("mssqlconnection"),
        b => b.MigrationsAssembly("HairArt")
    );
}); //servis kaydı yapıldı.
//AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddScoped<IRepositoryManager,RepositoryManager>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();
builder.Services.AddScoped<IScheduleRepository,ScheduleRepository>();
builder.Services.AddScoped<IEmployeeScheduleRepository,EmployeeScheduleRepository>();
builder.Services.AddScoped<IEmployeeProductRepository, EmployeeProductRepository>();


builder.Services.AddScoped<IServiceManager,ServiceManager>();
builder.Services.AddScoped<IProductService,ProductManager>();
builder.Services.AddScoped<ICategoryService,CategoryManager>();
builder.Services.AddScoped<IEmployeeService,EmployeeManager>();
builder.Services.AddScoped<IScheduleService,ScheduleManager>();
builder.Services.AddScoped<IEmployeeScheduleService,EmployeeScheduleManager>();
builder.Services.AddScoped<IEmployeeProductService, EmployeeProductManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
endpoints.MapAreaControllerRoute(
    name:"Admin",
    areaName:"Admin",
    pattern:"Admin/{controller=Dashboard}/{action=Index}/{id?}"

);
endpoints.MapControllerRoute(
     name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
