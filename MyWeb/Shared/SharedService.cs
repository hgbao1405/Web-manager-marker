using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared
{
    public static class SharedService
    {
        public static IServiceCollection AddSharedService(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers()
               .ConfigureApplicationPartManager(manager =>
               {
                   manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
               });
            return services;
        }
        public static IServiceCollection AddDatabaseContext<T>(this IServiceCollection services, string connectionString) where T : DbContext
        {
            services.AddMSSQL<T>(connectionString);
            return services;
        }

        private static IServiceCollection AddMSSQL<T>(this IServiceCollection services, string connectionString) where T : DbContext
        {
            services.AddDbContext<T>(m => m.UseSqlServer(connectionString, e => e.MigrationsAssembly(typeof(T).Assembly.FullName)));
            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<T>();
            dbContext.Database.Migrate();
            return services;
        }
    }
}