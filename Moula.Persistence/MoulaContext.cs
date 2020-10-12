using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Moula.Application.Contracts;
using Moula.Domain.Entities;

namespace Moula.Persistence
{
    public class MoulaContext : DbContext, IMoulaContext
    {
        public MoulaContext(DbContextOptions<MoulaContext> options)
            : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken) =>
            Database.BeginTransactionAsync(cancellationToken);

        public void CommitTransaction() => Database.CommitTransaction();
        public void RollbackTransaction() => Database.RollbackTransaction();

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MoulaContext).Assembly);

    }
}
