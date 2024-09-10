using Microsoft.EntityFrameworkCore;
using Route.BLL.Interfaces;
using Route.BLL.Repositories;
using Route.DAL.Context;

namespace Route.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*
             * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
             * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
             * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\ Demo of the session \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
             * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
             * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
             * 
             * Solution Contains 3 Projects Called 3-Tiers Arch.
             *      1. Presentation Layer   (MVC Project)           => Has Reference From Business Logic Layer.
             *          Contains => {
             *              a) Asp MVC 
             *              b) DeskTop App
             *              c) Mobile App
             *          }
             *      2. Business Logic Layer (Class Library Project) => Has Reference From Data Access Layer.
             *      3. Data Access Layer    (Class Library Project) 
             *          Contains => {
             *              a) Contexts
             *              b) Entities
             *              c) Migrations
             *          }
             *          
             * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
             * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
             * 
             * Types Of Injection (Dependency Injection).
             *      1. Singelton => Create One Object During Life-Time Cycle Of Application (Will Be Shared With All Requests)
             *      2. Scoped    => Create One Object During Life-Time Cycle Of Request
             *      3. Transient => Create Object Every Time You Request One
             * 
            */
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<RouteDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}