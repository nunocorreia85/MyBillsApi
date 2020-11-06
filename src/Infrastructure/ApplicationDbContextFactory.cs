using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using MyBills.Infrastructure.Persistence;

namespace MyBills.Infrastructure
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            Console.WriteLine("running ApplicationDbContextFactory.CreateDbContext");
            Console.WriteLine("args:");
            foreach (var arg in args)
            {
                Console.WriteLine(arg);
            }
            
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(
                Environment.GetEnvironmentVariable("SQLConnectionString"));

            return new ApplicationDbContext(optionsBuilder.Options, null, null);
        }
    }
}