

namespace Transaction.Service.Classes;

public class TransactionService : ITransactionService, IHandleMessages<Transfered>
{
    private readonly ITransactionData _transactionData;
    private readonly IMapper _mapper;
    static ILog log = LogManager.GetLogger<TransactionService>();

    public TransactionService(ITransactionData transactionData)
    {
        _transactionData = transactionData;
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutoMapperProfile>();
        });
        _mapper = config.CreateMapper();
    }

    public async Task<bool> AddTransactionAsync(TransactionDTO transactionDTO, IMessageSession messageSession)
    {
        Data.Entities.Transaction transaction = _mapper.Map<Data.Entities.Transaction>(transactionDTO);
        transaction.Status = eStatus.processing;
        transaction.Date = DateTime.UtcNow;
        Guid transactionId = await _transactionData.AddTransactionAsync(transaction);
        if (transactionId != Guid.Empty)
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
        bool isUpdated= await _transactionData.UpdateTransactionAsync(message.TransactionId, message.Status, message.FailureReason);
        if(!isUpdated)
            log.Info($"Failed to update status for Transaction, TransactionId = {message.TransactionId}");
        else
            log.Info($"Successfully updated status for Transaction, TransactionId = {message.TransactionId}");
    }

}

   

