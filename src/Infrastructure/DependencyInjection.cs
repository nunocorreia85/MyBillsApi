using Microsoft.EntityFrameworkCore;
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
            var cosmosEndpoint = Environment.GetEnvironmentVariable("CosmosDb_Endpoint");
            var cosmosKey = Environment.GetEnvironmentVariable("CosmosDb_Key");
            var cosmosDbName = Environment.GetEnvironmentVariable("CosmosDb_DbName");

            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(
                options => options.UseCosmos(cosmosEndpoint, cosmosKey, cosmosDbName));

            services.AddScoped<IDomainEventService, DomainEventService>();

            services.AddTransient<IDateTime, DateTimeService>();

            return services;
        }
    }
}