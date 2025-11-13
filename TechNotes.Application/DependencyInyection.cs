using Microsoft.Extensions.DependencyInjection;

namespace TechNotes.Application
{
    public static class DependencyInyection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(DependencyInyection).Assembly);
            });
            
            return services;
        }
    }
}
