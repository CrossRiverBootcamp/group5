using Account.Data.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Data
{
    public class OperationsHistoryData : IOperationsHistoryData
    {

        private readonly IDbContextFactory<AccountDbContext> _factory;
        public OperationsHistoryData(IDbContextFactory<AccountDbContext> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task<List<Entities.Account>> GetOperationsHistoty(Guid AccountID)
        {
            using var context = _factory.CreateDbContext();
            List<Entities.Account> operations = await context.Accounts.Where(account => account.Id == AccountID).ToListAsync();
            return operations;
        }
    }
}
