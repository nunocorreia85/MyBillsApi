using System;
using System.Collections;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MyBills.Infrastructure.Persistence;

namespace MyBills.Infrastructure
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var environmentVariables = Environment.GetEnvironmentVariables();
            foreach (DictionaryEntry dictionaryEntry in environmentVariables)
            {
                Console.WriteLine("Key: " + dictionaryEntry.Key + " value: " + dictionaryEntry.Value);
            }
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(
                Environment.GetEnvironmentVariable("SqlConnectionString"));

            return new ApplicationDbContext(optionsBuilder.Options, null, null);
        }
    }
}