using NSB.Messages;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Service;

namespace Transaction.NSB
{
    public class TransactionPolicy : Saga<TransactionPolicyData>, IAmStartedByMessages<TransactionAdded>
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
                FromAccountID=message.FromAccountID,
                ToAccountID=message.ToAccountID,
                Amount=message.Amount
            };
            await context.Publish(makeTransfer).ConfigureAwait(false);
            await UpdateTransaction();
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TransactionPolicyData> mapper)
        {
            mapper.MapSaga(sagaData => sagaData.TransactionId)
                .ToMessage<TransactionAdded>(message => message.TransactionId);
                //.ToMessage<TrackingAdded>(message => message.CardId);
        }
        private async Task UpdateTransaction()
        {
            if (Data.IsTransactionAdded && Data.IsTransfered)
            {
                await _transactionService.UpdateTransactionAsync();
                MarkAsComplete();
            }
        }
    }
}
