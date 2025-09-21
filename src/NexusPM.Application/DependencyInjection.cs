using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NexusPM.Application.Common.Behaviours;

namespace NexusPM.Application
{
    /// <summary>
    /// Provides extension methods for registering application services.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers application services and validators in the dependency injection container.
        /// </summary>
        /// <param name="services">The service collection to add services to.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenRequestPreProcessor(typeof(LoggingBehaviour<>));
                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            });
            return services;
        }
    }
}