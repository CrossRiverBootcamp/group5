using AutoMapper;
using NSB.Messages;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data;
using Transaction.Service.DTO;
using Transaction.Service.Models;

namespace Transaction.Service
{
    public class TransactionService: ITransactionService
    {
        private readonly ITransactionData _transactionData;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionData transactionData, IMapper mapper)
        {
            _transactionData = transactionData;
            _mapper = mapper;
        }

        public async Task<bool> AddTransactionAsync(TransactionDTO transactionDTO, IMessageSession messageSession)
        {
            Data.Entities.Transaction transaction = _mapper.Map<Data.Entities.Transaction>(transactionDTO);
            transaction.Status = "Processing";
            transaction.Date = DateTime.UtcNow;
            Guid transactionId =await _transactionData.AddTransactionAsync(transaction);
            TransactionAdded transactionAdded = _mapper.Map<TransactionAdded>(transactionDTO);
            transactionAdded.TransactionId = transactionId;
            await messageSession.Publish(transactionAdded).ConfigureAwait(false);
            return true; //???????

        }

        public  Task UpdateTransactionAsync(UpdateTransactionModel updateTransactionModel)
        {
            //how to send the object to DL?
            return  _transactionData.UpdateTransactionAsync(updateTransactionModel.TransactionId, updateTransactionModel.Status, updateTransactionModel.FailureReason);
        }
    }
}
