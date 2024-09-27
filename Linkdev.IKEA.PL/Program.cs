using Linkdev.IKEA.BLL.Services.Departments;
using Linkdev.IKEA.BLL.Services.Employees;
using Linkdev.IKEA.DAL.Presistance.Data;
using Linkdev.IKEA.DAL.Presistance.Repositories.Departments;
using Linkdev.IKEA.DAL.Presistance.Repositories.Employees;
using Linkdev.IKEA.DAL.Presistance.UnitOfWork;
using Linkdev.IKEA.PL.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Linkdev.IKEA.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            #region Configure Services
            
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(
                (optionsBuilder) => optionsBuilder.UseLazyLoadingProxies()
                                                  .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), 
                (migrationOptions) => migrationOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            

            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            builder.Services.AddAutoMapper(config => config.AddProfile<MappingProfile>());
            

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            #region Configure Kestral Middlewares

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"); 

            #endregion

            app.Run();
        }
    }
}
