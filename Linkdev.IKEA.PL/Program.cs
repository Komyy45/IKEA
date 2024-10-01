using Linkdev.IKEA.BLL.Common.Services.AttachmentService;
using Linkdev.IKEA.BLL.Services.Departments;
using Linkdev.IKEA.BLL.Services.Employees;
using Linkdev.IKEA.DAL.Entities.Identity;
using Linkdev.IKEA.DAL.Presistance.Data;
using Linkdev.IKEA.DAL.Presistance.Repositories.Departments;
using Linkdev.IKEA.DAL.Presistance.Repositories.Employees;
using Linkdev.IKEA.DAL.Presistance.UnitOfWork;
using Linkdev.IKEA.PL.Mapping;
using Microsoft.AspNetCore.Identity;
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

            builder.Services.AddTransient<IAttachmentService, AttachmentService>();

            builder.Services.AddAutoMapper(config => config.AddProfile<MappingProfile>());

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(config =>
                {
                    #region Password
                    
                    config.Password.RequireNonAlphanumeric = true;
                    config.Password.RequiredLength = 6;
                    config.Password.RequireUppercase = true;
                    config.Password.RequireLowercase = true;
                    config.Password.RequiredUniqueChars = 1; 

                    #endregion

                    #region User

                    config.User.RequireUniqueEmail = true;
                    //config.User.AllowedUserNameCharacters = ""; 

                    #endregion

                    #region Lockout
                    
                    config.Lockout.AllowedForNewUsers = true;
                    config.Lockout.MaxFailedAccessAttempts = 5;
                    config.Lockout.DefaultLockoutTimeSpan = new TimeSpan(5, 0, 0, 0); 

                    #endregion

                }).AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LogoutPath = "/Account/SignIn";
                options.LoginPath = "/Account/SignIn";
                options.AccessDeniedPath = "/Home/Error";
            });

            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = "Identity.Application";
            //    options.DefaultChallengeScheme = "Identity.Application";
            //})
            //.AddCookie("Hamada", options =>
            //{
            //    options.LoginPath = "/Account/HamadaYl3b";
            //    options.LogoutPath = "/Account/Logout";
            //    options.AccessDeniedPath = "/Home/Error";
            //});

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

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"); 

            #endregion

            app.Run();
        }
    }
}
