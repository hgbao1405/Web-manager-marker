using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using MyWeb.Data;
using MyWeb.Areas.Identity.Data;
using CharacterModule;
using Shared;

namespace MyWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var config = builder.Configuration;

            var connectionString = builder.Configuration.GetConnectionString("MyWebContext") ?? throw new InvalidOperationException("Connection string 'MyWebContextConnection' not found.");

            builder.Services.AddDbContext<MyWebContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<MyWebUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MyWebContext>();

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddControllersWithViews();
            builder.Services.AddCharacterService(connectionString);

            builder.Services.AddSharedService(config);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting(); // Kích hoạt middleware định tuyến

            // Thiết lập các file tĩnh (static files)
            app.UseStaticFiles();

            // Sử dụng middleware xác thực và ủy quyền (authentication and authorization)
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers(); // Đăng ký các controllers

            // Đăng ký endpoints cho các controllers và area

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "admin",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                  );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller}/{action=Index}/{id?}");
            });

            app.Run();
        }
    }
}