using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using MyBills.Api;
using MyBills.Application;
using MyBills.Infrastructure;

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