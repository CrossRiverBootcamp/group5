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
        Task<string> AddCustomerAsync(CustomerDTO customerDTO);
        Task<Transfered> CheckAndTransfer_AddOperations(MakeTransfer message);
        Task<bool> AddOperation(MakeTransfer makeTransfer);
    }
}
