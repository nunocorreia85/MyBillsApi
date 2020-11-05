using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MyBills.Infrastructure.Persistence;

namespace MyBills.Infrastructure
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(
                Environment.GetEnvironmentVariable("SqlConnectionString", EnvironmentVariableTarget.Process));

            return new ApplicationDbContext(optionsBuilder.Options, null, null);
        }
    }
}