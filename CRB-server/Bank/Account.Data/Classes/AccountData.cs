﻿using Account.Data.EF;
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

        public async Task<bool> DoesAccountExist(Guid accountId)
        {
            var context = _factory.CreateDbContext();
            return await context.Accounts.AnyAsync(account => account.Id == accountId);
        }

        public Task<bool> IsBalanceGreater(Guid accountId, int amount)
        {
            var context = _factory.CreateDbContext();
            return context.Accounts.AnyAsync(account => account.Id == accountId && account.Balance >= amount);
        }

        public async Task<bool> TransactionBetweenAccountsAsync(Guid fromAccountId, Guid toAccountId, int amount)
        {
            using (var context = _factory.CreateDbContext())
            {
                Entities.Account fromAccount = await context.Accounts.FindAsync(fromAccountId);
                fromAccount.Balance -= amount;
                Entities.Account toAccount = await context.Accounts.FindAsync(toAccountId);
                toAccount.Balance += amount;
                await context.SaveChangesAsync();
                return true;
                //false?
            }
        }
        public async Task<int> GetBalanceByAccountIdAsync(Guid accountId)
        {
            using (var context = _factory.CreateDbContext())
            {
                Entities.Account account = await context.Accounts.FindAsync(accountId);
                //check account is null?
                return account.Balance;

            }
        }

        public async Task<bool> AddOperation(Operation operationFromAccount, Operation operationToAccount)
        {
            using (var context = _factory.CreateDbContext())
            {
                await context.Operations.AddAsync(operationFromAccount);
                await context.Operations.AddAsync(operationToAccount);
                await context.SaveChangesAsync();
                return true;
                //false?
            }
        }
    }
}
