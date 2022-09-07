using Account.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Service
{
    public interface IAccountService
    {
        Task<bool> AddCustomerAsync(CustomerDTO customerDTO);
        Task<bool> DoesAccountExist(Guid accountId);
        Task<bool> IsBalanceGreater(Guid accountId, int amount);
        Task<bool> TransactionBetweenAccountsAsync(Guid fromAccountId, Guid toAccountId, int amount);
    }
}
