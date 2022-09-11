using AutoMapper;
using NSB.Messages;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data;
using Transaction.Service.DTO;
using Transaction.Service.Models;
using Transaction.Services;

namespace Transaction.Service
{
    public class TransactionService: ITransactionService, IHandleMessages<Transfered>
    {
        private readonly ITransactionData _transactionData;
        private readonly IMapper _mapper;
        static ILog log = LogManager.GetLogger<TransactionService>();

        public TransactionService(ITransactionData transactionData)//, IMapper mapper)
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
            if(transactionId != Guid.Empty) 
            { 
                TransactionAdded transactionAdded = _mapper.Map<TransactionAdded>(transactionDTO);
                transactionAdded.TransactionId = transactionId;
                await messageSession.Publish(transactionAdded).ConfigureAwait(false);
                return true;
            }
           return false;

        }

        public async Task Handle(Transfered message, IMessageHandlerContext context)
        {
            log.Info($"Update status for Transaction, TransactionId = {message.TransactionId}");
            UpdateTransactionModel updateTransactionModel = _mapper.Map<UpdateTransactionModel>(message);
            await UpdateTransactionAsync(updateTransactionModel);

        }

        private Task UpdateTransactionAsync(UpdateTransactionModel updateTransactionModel)
        {
            //how to send the object to DL?
            return  _transactionData.UpdateTransactionAsync(updateTransactionModel.TransactionId, updateTransactionModel.Status, updateTransactionModel.FailureReason);
        }
    }
}
