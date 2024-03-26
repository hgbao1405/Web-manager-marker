using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterRepo.Data;
using System.Reflection;
using MediatR;

namespace CharacterRepo
{
    public static class CharacterRepoService
    {
        public static IServiceCollection AddCharacterRepoService(this IServiceCollection services, string connectionString)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services.AddDatabaseContext<MyWebContext>(connectionString)
                .AddScoped<IMyWebContext>(provider => provider.GetService<MyWebContext>());
        }
    }
}