using NSB.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Data.Interfaces
{
    public interface ITransactionData
    {
        Task<Guid> AddTransactionAsync(Entities.Transaction transaction);
        Task UpdateTransactionAsync(Guid transactionId, eStatus status, string failureReason);
    }
}
