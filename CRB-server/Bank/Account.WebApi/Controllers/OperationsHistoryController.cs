
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Account.WebApi.Controllers;

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
    [HttpGet("GetOperationsList/{accountID}/{pageNumber}/{numberOfRecords}")]
    public async Task<ActionResult> GetOperationsList(Guid accountID, int pageNumber,int numberOfRecords)
    {
        List<OperationDTO> operationsDTO = 
            await _operationsHistoryService.GetOperationsHistotyListAsync(accountID, pageNumber, numberOfRecords);
        return operationsDTO == null ? NotFound() : operationsDTO.Count == 0 ? NoContent() : Ok(operationsDTO);
    }

    [HttpGet("GetAccountInfo/{accountID}")]
    public async Task<ActionResult> GetAccountInfo(Guid accountID)
    {
        AccountInfoDTO accountInfoDTO = await _operationsHistoryService.GetAccountInfo(accountID);
        return accountInfoDTO != null ? Ok(accountInfoDTO) : NotFound();
    }

}
