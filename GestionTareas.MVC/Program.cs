using GestionTareas.Api.Models;
using GestionTareas.ApiConsumer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace GestionTareas.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Crud<Usuario>.EndPoint = "https://localhost:7244/api/Usuarios"; 
            Crud<Tarea>.EndPoint = "https://localhost:7244/api/Tareas";
            Crud<Proyecto>.EndPoint = "https://localhost:7244/api/Proyectos";
            var builder = WebApplication.CreateBuilder(args);
          
            
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/Login";
                });

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
