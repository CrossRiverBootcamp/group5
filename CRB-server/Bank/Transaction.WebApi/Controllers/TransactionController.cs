using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using Transaction.Service;
using Transaction.Service.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Transaction.WebApi.Controllers
{
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
        [HttpPost]
        public async Task<ActionResult> AddTransactionAsync([FromBody] TransactionDTO transactionDTO)
        {
            return Ok(_transactionService.AddTransactionAsync(transactionDTO, _messageSession));//????
        }

    }
}
