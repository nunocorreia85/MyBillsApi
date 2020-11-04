using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBills.Application.Common.Interfaces;
using MyBills.Infrastructure.Persistence;
using MyBills.Infrastructure.Services;
using System;

namespace MyBills.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            if (bool.TryParse(Environment.GetEnvironmentVariable("UseInMemoryDatabase"), out var useInMemoryDatabase) 
                && useInMemoryDatabase)
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("MyBillsDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        Environment.GetEnvironmentVariable("SqlConnectionString"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IDomainEventService, DomainEventService>();

            services.AddTransient<IDateTime, DateTimeService>();

            return services;
        }
    }
}