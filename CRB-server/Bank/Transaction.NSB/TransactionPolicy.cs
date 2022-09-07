using NSB.Messages;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.NSB
{
    public class TransactionPolicy : Saga<TransactionPolicyData>, IAmStartedByMessages<TransactionAdded>
    {
        static ILog log = LogManager.GetLogger<TransactionPolicy>();

        public TransactionPolicy()
        {

        }

        public Task Handle(TransactionAdded message, IMessageHandlerContext context)
        {
            log.Info($"Add Transaction, TransactionId = {message.TransactionId}");
            Data.IsTransactionAdded=true;
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TransactionPolicyData> mapper)
        {
            mapper.MapSaga(sagaData => sagaData.TransactionId)
                .ToMessage<TransactionAdded>(message => message.TransactionId);
                //.ToMessage<TrackingAdded>(message => message.CardId);
        }
    }
}
