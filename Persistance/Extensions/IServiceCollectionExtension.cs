using Task.Domain;
using Microsoft.Extensions.DependencyInjection;
using Task.Persistance.Interfaces;
using Task.Persistance.Repositories;

namespace Persistance.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IRepository<User>, Repository<User>>();
            return services;
        }
    }
}
