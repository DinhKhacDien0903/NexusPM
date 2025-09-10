using Microsoft.Extensions.DependencyInjection;
using NexusPM.Application.Abstractions.Security;

namespace NexusPM.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}
