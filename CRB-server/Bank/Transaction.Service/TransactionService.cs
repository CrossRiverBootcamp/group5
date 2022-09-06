using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data;
using Transaction.Service.DTO;

namespace Transaction.Service
{
    public class TransactionService: ITransactionService
    {
        private readonly ITransactionData _transactionData;

        public TransactionService(ITransactionData transactionData)
        {
            _transactionData = transactionData;
        }

        public Task AddTransactionAsync(TransactionDTO transactionDTO)
        {
            throw new NotImplementedException();
        }
    }
}
