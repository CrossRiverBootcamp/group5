using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data.EF;

namespace Transaction.Data
{
    public class TransactionData: ITransactionData
    {
        private readonly IDbContextFactory<TransactionDbContext> _factory;
        public TransactionData(IDbContextFactory<TransactionDbContext> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task<Guid> AddTransactionAsync(Data.Entities.Transaction transaction)
        {
            using (var context = _factory.CreateDbContext())
            {
                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();   
                return transaction.Id;
            }
        }

        public async Task UpdateTransactionAsync(Guid transactionId, string status, string failureReason)
        {
            using (var context = _factory.CreateDbContext())
            {
                Entities.Transaction transaction = await context.Transactions.FindAsync(transactionId);
                transaction.Status = status;
                transaction.FailureReason = failureReason;
                await context.SaveChangesAsync();              
            }
        }
    }
}
