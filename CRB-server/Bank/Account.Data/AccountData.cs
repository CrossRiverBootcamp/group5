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
    public class AccountData : IAccountData
    {
        private readonly IDbContextFactory<AccountDbContext> _factory;
        public AccountData(IDbContextFactory<AccountDbContext> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
            using(var context = _factory.CreateDbContext())
            {
               
                Entities.Account account = await context.Accounts.Include(account => account.Customer)
                    .FirstOrDefaultAsync(account => account.Customer.Email.Equals(email));
                if (account == null)
                {
                    return true;
                }
                return false;
            }
        }
        public async Task<bool> AddCustomerAsync(Customer customer)
        {
            using (var context = _factory.CreateDbContext())
            {
                await context.Customers.AddAsync(customer);
                await context.SaveChangesAsync();
                return true;
                //false?
            }
        }

        public async Task<bool> CreateAccountAsync(Entities.Account account)
        {
            using (var context = _factory.CreateDbContext())
            {
                await context.Accounts.AddAsync(account);
                await context.SaveChangesAsync();
                return true;
                //false?
            }
        }

        public Task<bool> DoesAccountExist(Guid accountId)
        {
            var context = _factory.CreateDbContext();
                return context.Accounts.AnyAsync(account => account.Id == accountId);
        }
    }
}
