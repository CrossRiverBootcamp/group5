using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Service.DTO;
using Transaction.Service.Models;

namespace Transaction.Service
{
    public interface ITransactionService
    {
        Task<bool> AddTransactionAsync(TransactionDTO transactionDTO, IMessageSession messageSession);
        Task UpdateTransactionAsync(UpdateTransactionModel updateTransactionModel);
    }
}
