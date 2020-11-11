using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBills.Application.Common.Interfaces;
using MyBills.Infrastructure.Persistence;
using MyBills.Infrastructure.Services;
using Serilog;

namespace MyBills.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            // Registering Serilog provider
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(logger));

            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>();

            services.AddScoped<IDomainEventService, DomainEventService>();

            services.AddTransient<IDateTime, DateTimeService>();

            return services;
        }
    }
}