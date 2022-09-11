using Account.Data.EF;
using Account.Data.Entities;
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

        public async Task<List<Operation>> GetOperationsHistoty(Guid accountID, int pageNumber, int numberOfRecords)
        {
            using var context = _factory.CreateDbContext();
                return await context.Operations.Where(operation => operation.AccountId == accountID)
                    .Skip(numberOfRecords*(pageNumber-1)).Take(numberOfRecords).ToListAsync();
        }
    }
}
