using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data.EF;
using Transaction.Data.Classes;
using Transaction.Data.Interfaces;
using Transaction.Service.Classes;
using Transaction.Service.Interfaces;

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

        public static void ExtensionMigrateDB(this IServiceCollection service, IServiceProvider appServices)
        {
            using (var scope = appServices.CreateScope())
            {
                var dataContext = scope.ServiceProvider.GetRequiredService<TransactionDbContext>();
                dataContext.Database.Migrate();
            }
        }
    }
}
