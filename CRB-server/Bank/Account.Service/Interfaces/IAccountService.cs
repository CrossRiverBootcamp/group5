using Account.Service.DTO;
using NSB.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Service.Interfaces
{
    public interface IAccountService
    {
        Task<bool> CreateVerificationCode(string email);
        Task<bool> AddCustomerAsync(CustomerDTO customerDTO);
        Task<bool> DoesAccountExist(Guid accountId);
        Task<bool> IsBalanceGreater(Guid accountId, int amount);
        Task<bool> TransactionBetweenAccountsAsync(Guid fromAccountId, Guid toAccountId, int amount);
        Task<bool> AddOperation(MakeTransfer makeTransfer);
    }
}
