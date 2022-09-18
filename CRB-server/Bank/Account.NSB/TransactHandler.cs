
namespace Account.NSB;

public class TransactHandler : IHandleMessages<MakeTransfer>
{
    static ILog log = LogManager.GetLogger<TransactHandler>();

    private readonly IAccountService _accountService;
    public TransactHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task Handle(MakeTransfer message, IMessageHandlerContext context)
    {
        Transfered transfered = await _accountService.CheckTransferAddOperationsAsync(message);
        log.Info($"{transfered.Status} ! the transfer from account: {message.FromAccountID} to account: {message.ToAccountID}");
        await context.Publish(transfered);
    }

}
