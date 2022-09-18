

namespace Transaction.NSB;

public class TransactionPolicy : Saga<TransactionPolicyData>, IAmStartedByMessages<TransactionAdded>, IHandleMessages<Transfered>
{
    static ILog log = LogManager.GetLogger<TransactionPolicy>();
    private readonly IMapper _mapper;

    public TransactionPolicy(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task Handle(TransactionAdded message, IMessageHandlerContext context)
    {
        log.Info($"Add Transaction, TransactionId = {message.TransactionId}");
        MakeTransfer makeTransfer = _mapper.Map<MakeTransfer>(message);
        await context.Send(makeTransfer).ConfigureAwait(false);
    }

    public async Task Handle(Transfered message, IMessageHandlerContext context)
    {
        log.Info($"Transferred Transaction, TransactionId = {message.TransactionId}");
       
        MarkAsComplete();
    }

    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TransactionPolicyData> mapper)
    {
        mapper.MapSaga(sagaData => sagaData.TransactionId)
            .ToMessage<TransactionAdded>(message => message.TransactionId)
            .ToMessage<Transfered>(message => message.TransactionId);
    }
    
}
