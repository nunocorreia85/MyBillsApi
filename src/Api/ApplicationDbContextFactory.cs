using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MyBills.Infrastructure.Persistence;

namespace MyBills.Api
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            Console.WriteLine("MyBills.Api.ApplicationDbContextFactory:");
            
            // var configuration = new ConfigurationBuilder()
            //     .SetBasePath(Directory.GetCurrentDirectory())
            //     .AddJsonFile("local.settings.json")
            //     .Build();
            
            var connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");
            
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString ?? throw new Exception("SqlConnectionString is empty"));

            //TODO: fix injection 
            return new ApplicationDbContext(optionsBuilder.Options, null, null, null);
        }
    }
}