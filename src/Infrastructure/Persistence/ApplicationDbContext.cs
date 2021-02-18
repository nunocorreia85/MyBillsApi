using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyBills.Application.Common.Interfaces;
using MyBills.Domain.Common;
using MyBills.Domain.Entities;

namespace MyBills.Infrastructure.Persistence
{
    public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;

        public ApplicationDbContext(
            DbContextOptions options,
            IDomainEventService domainEventService,
            IDateTime dateTime,
            IConfiguration configuration) : base(options)
        {
            _domainEventService = domainEventService;
            _dateTime = dateTime;
            _configuration = configuration;
        }

        public DbSet<BankTransaction> BankTransactions { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        //entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime.Now;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        //entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents(cancellationToken);

            return result;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (builder.IsConfigured)
            {
                return;
            }

            if (bool.TryParse(_configuration["Values:UseInMemoryDatabase"], out var useInMemDb) && useInMemDb)
            {
                builder.UseInMemoryDatabase("MyBillsDb");
            }
            else
            {
                builder.UseSqlServer(_configuration.GetConnectionString("SqlConnectionString"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        private async Task DispatchEvents(CancellationToken cancellationToken)
        {
            var domainEventEntities = ChangeTracker.Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .ToArray();

            foreach (var domainEvent in domainEventEntities)
            {
                await _domainEventService.PublishAsync(domainEvent, cancellationToken);
            }
        }
    }
}