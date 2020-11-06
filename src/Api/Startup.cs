using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using MyBills.Api;
using MyBills.Application;
using MyBills.Application.Common.Interfaces;
using MyBills.Infrastructure;
using MyBills.Infrastructure.Persistence;

[assembly: FunctionsStartup(typeof(Startup))]

namespace MyBills.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var context = builder.GetContext();
            
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(context.Configuration);
        }
    }
}