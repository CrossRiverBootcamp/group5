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

        public Task AddTransactionAsync()
        {
            using (var context = _factory.CreateDbContext())
            {
                return Task.CompletedTask;
            }
        }
    }
}
