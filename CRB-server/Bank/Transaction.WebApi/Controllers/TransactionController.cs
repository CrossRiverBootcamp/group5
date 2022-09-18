
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Transaction.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    private IMessageSession _messageSession;

    public TransactionController(ITransactionService transactionService, IMessageSession messageSession)
    {
        _transactionService = transactionService;
        _messageSession = messageSession;
    }

    // POST api/<TransactionController>
    [HttpPost("AddTransaction")]
    public async Task<ActionResult> AddTransactionAsync([FromBody] TransactionDTO transactionDTO)
    {
        bool isTransactionAdded = await _transactionService.AddTransactionAsync(transactionDTO, _messageSession);
        return isTransactionAdded ? Accepted("Transaction successfully added in DB") : BadRequest("failed to add transaction in DB");
    }

}
