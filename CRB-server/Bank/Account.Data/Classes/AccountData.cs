using Account.Data.EF;
using Account.Data.Entities;
using Account.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Data.Classes
{
    public class AccountData : IAccountData
    {
        private readonly IDbContextFactory<AccountDbContext> _factory;
        public AccountData(IDbContextFactory<AccountDbContext> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task<bool> CreateEmailVerification(EmailVerification emailVerification)
        {
            using (var context = _factory.CreateDbContext())
            {
                EmailVerification existsVerification = await context.EmailVerifications.FindAsync(emailVerification.Email);
                if(existsVerification != null)
                {
                    existsVerification.VerificationCode = emailVerification.VerificationCode;
                    existsVerification.ExpirationTime = emailVerification.ExpirationTime;
                }
                else
                    await context.EmailVerifications.AddAsync(emailVerification);
                await context.SaveChangesAsync();
                return true;
                //false?
            }
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
            using (var context = _factory.CreateDbContext())
            {

                Entities.Account account = await context.Accounts.Include(account => account.Customer)
                    .FirstOrDefaultAsync(account => account.Customer.Email.Equals(email));
                if (account == null)
                {
                    return false;
                }
                return true;
            }
        }

        public async Task<bool> ValidVerificationCode(string email, int code)//?????
        {
            using (var context = _factory.CreateDbContext())
            {
                EmailVerification emailVerification = await context.EmailVerifications  
                    .FirstOrDefaultAsync(verification => verification.Email.Equals(email) 
                && verification.VerificationCode == code );
               
                if(emailVerification != null)
                {                   
                    if (DateTime.Compare(emailVerification.ExpirationTime, DateTime.Now)>=0)
                        return true;
                }
                    return false;
            }
        }

        public async Task<bool> AddCustomerAsync(Customer customer)
        {
            try
            {
                using (var context = _factory.CreateDbContext())
                {
                    await context.Customers.AddAsync(customer);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch
            {
                return false;
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

        public async Task<bool> DoBothAccountsExist(Guid fromAccountId, Guid toAccountId)
        {
            var context = _factory.CreateDbContext();
            bool fromRes =  await context.Accounts.AnyAsync(account => account.Id == fromAccountId);
            bool toRes = await context.Accounts.AnyAsync(account => account.Id == toAccountId);
            return fromRes && toRes; 
        }

        public async Task<bool> IsBalanceGreater(Guid accountId, int amount)
        {
            var context = _factory.CreateDbContext();
            return await context.Accounts.AnyAsync(account => account.Id == accountId && account.Balance >= amount);
        }

        public async Task<bool> TransactionBetweenAccountsAndAddOperationAsync(Guid fromAccountId, Guid toAccountId, int amount, Operation operationFromAccount, Operation operationToAccount)
        {
            using (var context = _factory.CreateDbContext())
            {
                Entities.Account fromAccount = await context.Accounts.FindAsync(fromAccountId);
                fromAccount.Balance -= amount;
                await context.Operations.AddAsync(operationFromAccount);
                Entities.Account toAccount = await context.Accounts.FindAsync(toAccountId);
                toAccount.Balance += amount;
                await context.Operations.AddAsync(operationToAccount);
                await context.SaveChangesAsync();
                return true;
            }
            return false; // "An error occurred while updating the data in the DB";
        }
        public int GetBalanceByAccountIdAsync(Guid accountId)
        {
            using (var context = _factory.CreateDbContext())
            {
                Entities.Account account = context.Accounts.Find(accountId);
                //check account is null?
                return account.Balance;

            }
        }

    }
}
