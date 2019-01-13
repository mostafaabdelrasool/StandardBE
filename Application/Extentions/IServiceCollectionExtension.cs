using Microsoft.Extensions.DependencyInjection;
using Task.Application.Users.Model;
using Task.Application.Interfaces;
using Task.Domain;
using Task.Application.Service;

namespace Task.Application.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IServiceQuery<User, UserDto>, 
                ServiceQuery<User, UserDto>>();
            services.AddTransient<ICommandService<User, UserDto>,
              CommandService<User, UserDto>>();
            return services;
        }
    }
}
