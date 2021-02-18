using System;
using System.IO;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using MyBills.Api;

namespace MyBills.Application.IntegrationTests.Infrastructure
{
    public class TestHost
    {
        public TestHost()
        {
            var startup = new TestStartup();
            var host = new HostBuilder()
                .ConfigureServices(ReplaceTestOverrides)
                .ConfigureWebJobs(startup.Configure)
                .Build();

            ServiceProvider = host.Services;
        }

        public IServiceProvider ServiceProvider { get; }

        private void ReplaceTestOverrides(IServiceCollection services)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json")
                .Build();
            services.Replace(new ServiceDescriptor(typeof(IConfiguration), configuration));
        }

        private class TestStartup : Startup
        {
            public override void Configure(IFunctionsHostBuilder builder)
            {
                SetExecutionContextOptions(builder);
                base.Configure(builder);
            }

            private static void SetExecutionContextOptions(IFunctionsHostBuilder builder)
            {
                builder.Services.Configure<ExecutionContextOptions>(o =>
                    o.AppDirectory = Directory.GetCurrentDirectory());
            }
        }
    }
}