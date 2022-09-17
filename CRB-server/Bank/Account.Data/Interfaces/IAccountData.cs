using Account.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Data.Interfaces;

public interface IAccountData
{
    Task<bool> CreateEmailVerification(EmailVerification emailVerification);
    Task<bool> IsEmailExistAsync(string email);
    Task<bool> ValidVerificationCode(string email, int code);
    Task<bool> AddCustomerAsync(Customer customer);
    Task<bool> CreateAccountAsync(Entities.Account account);
    Task<bool> DoBothAccountsExist(Guid fromAccountId, Guid toAccountId);
    Task<bool> IsBalanceGreater(Guid accountId, int amount);
    Task<bool> TransactionBetweenAccountsAndAddOperationAsync(Guid fromAccountId, Guid toAccountId, int amount, Operation operationFromAccount, Operation operationToAccount);
    int GetBalanceByAccountIdAsync(Guid accountId);
}
