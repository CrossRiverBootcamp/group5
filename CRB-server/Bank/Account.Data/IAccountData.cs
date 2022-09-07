using Account.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Data
{
    public interface IAccountData
    {
        Task<bool> IsEmailExistAsync(string email);
        Task<bool> AddCustomerAsync(Customer customer);
        Task<bool> CreateAccountAsync(Entities.Account account);
        Task<bool> DoesAccountExist(Guid accountId);
        Task<bool> IsBalanceGreater(Guid accountId, int amount);
        Task<bool> TransactionBetweenAccountsAsync(Guid fromAccountId, Guid toAccountId, int amount);
    }
}
