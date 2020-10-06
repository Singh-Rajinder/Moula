using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Moula.Domain.Entities;

namespace Moula.Application.Contracts
{
    public interface IMoulaContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Payment> Payments { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        int SaveChanges();
        
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
        void CommitTransaction();
        void RollbackTransaction();
    }
}
