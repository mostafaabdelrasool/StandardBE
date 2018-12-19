using Microsoft.Extensions.DependencyInjection;
using Task.Application.User.Model;
using Task.Application.Interfaces;
using Task.Domain;

namespace Task.Application.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IServiceQuery<Task.Domain.User, UserDto>, 
                ServiceQuery<Task.Domain.User, UserDto>>();
            services.AddTransient<ICommandService<Task.Domain.User, UserDto>,
              ICommandService<Task.Domain.User, UserDto>>();
            return services;
        }
    }
}
