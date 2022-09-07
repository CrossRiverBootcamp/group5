using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Data
{
    public interface ITransactionData
    {
        Task<Guid> AddTransactionAsync(Data.Entities.Transaction transaction);
        Task UpdateTransactionAsync(Guid transactionId,string status, string failureReason);

    }
}
