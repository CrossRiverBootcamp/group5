using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Service.DTO;

namespace Transaction.Service
{
    public interface ITransactionService
    {
        Task<bool> AddTransactionAsync(TransactionDTO transactionDTO, IMessageSession messageSession);
    }
}
