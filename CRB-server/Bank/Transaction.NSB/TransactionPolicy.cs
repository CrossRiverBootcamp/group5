using NSB.Messages;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Service;
using Transaction.Service.Models;

namespace Transaction.NSB
{
    public class TransactionPolicy : Saga<TransactionPolicyData>, IAmStartedByMessages<TransactionAdded>, IHandleMessages<Transfered>
    {
        static ILog log = LogManager.GetLogger<TransactionPolicy>();
        private readonly ITransactionService _transactionService;

        public TransactionPolicy(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task Handle(TransactionAdded message, IMessageHandlerContext context)
        {
            log.Info($"Add Transaction, TransactionId = {message.TransactionId}");
            Data.IsTransactionAdded=true;
            //mapper?
            MakeTransfer makeTransfer = new MakeTransfer()
            {
                TransactionId = message.TransactionId,
                FromAccountID=message.FromAccountID,
                ToAccountID=message.ToAccountID,
                Amount=message.Amount
            };
            await context.Publish(makeTransfer).ConfigureAwait(false);
            await UpdateTransaction();
        }

        public async Task Handle(Transfered message, IMessageHandlerContext context)
        {
            log.Info($"Transferred Transaction, TransactionId = {message.TransactionId}");
            Data.IsTransferred = true;
            Data.Status = message.Status;
            Data.FailureReason = message.FailureReason;
            await UpdateTransaction();
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TransactionPolicyData> mapper)
        {
            mapper.MapSaga(sagaData => sagaData.TransactionId)
                .ToMessage<TransactionAdded>(message => message.TransactionId)
                .ToMessage<Transfered>(message => message.TransactionId);
        }
        private async Task UpdateTransaction()
        {
            if (Data.IsTransactionAdded && Data.IsTransferred)
            {
                //mapper?
                UpdateTransactionModel updateTransactionModel = new UpdateTransactionModel()
                {
                    TransactionId=Data.TransactionId,
                    Status=Data.Status,
                    FailureReason=Data.FailureReason
                };
                await _transactionService.UpdateTransactionAsync(updateTransactionModel);
                MarkAsComplete();
            }
        }
    }
}
