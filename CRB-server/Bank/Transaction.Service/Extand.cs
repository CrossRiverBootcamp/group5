using Microsoft.Extensions.DependencyInjection;
using Transaction.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data.EF;

namespace Transaction.Service
{
    public static class Extand
    {
        public static void ExtensionsDI(this IServiceCollection service)
        {
            service.AddScoped<ITransactionData, TransactionData>();
            service.AddScoped<ITransactionService, TransactionService>();

        }
        public static void ExtensionAddDbContext(this IServiceCollection service, string Connection)
        {
            service.AddDbContextFactory<TransactionDbContext>(item =>
                item.UseSqlServer(Connection));
          
        }
    }
}
