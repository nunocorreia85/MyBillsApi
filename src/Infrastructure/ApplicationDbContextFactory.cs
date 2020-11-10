using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MyBills.Infrastructure.Persistence;

namespace MyBills.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            Console.WriteLine("MyBills.Api.ApplicationDbContextFactory:");

            var connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString ?? throw new Exception("SqlConnectionString is empty"));

            return new ApplicationDbContext(optionsBuilder.Options, null, null, null);
        }
    }
}