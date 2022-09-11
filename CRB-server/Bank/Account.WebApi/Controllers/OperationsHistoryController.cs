using Account.Service;
using Account.Service.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Account.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsHistoryController : ControllerBase
    {
        private readonly IOperationsHistoryService _operationsHistoryService;

        public OperationsHistoryController(IOperationsHistoryService operationsHistoryService)
        {
            _operationsHistoryService = operationsHistoryService;
        }

        // GET: api/<OperationsHistoryController>
        [HttpGet("GetOperationsList/{id}")]
        public async Task<ActionResult<List<OperationDTO>>> GetOperationsList(Guid accountID)
        {
            List<OperationDTO> operationsDTO = await _operationsHistoryService.GetOperationsHistotyListAsync(accountID);
            if (operationsDTO == null)
                return NoContent();
            return Ok(operationsDTO);
        }

    }
}
