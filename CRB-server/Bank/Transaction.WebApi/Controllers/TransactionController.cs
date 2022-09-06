using Microsoft.AspNetCore.Mvc;
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

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // POST api/<TransactionController>
        [HttpPost]
        public void AddTransactionAsync([FromBody] TransactionDTO transactionDTO)
        {
        }

    }
}
