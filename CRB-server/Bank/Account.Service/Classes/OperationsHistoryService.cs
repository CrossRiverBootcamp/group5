
namespace Account.Service.Classes;

public class OperationsHistoryService : IOperationsHistoryService
{
    private readonly IOperationsHistoryData _OperationsHistoryData;
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;

    public OperationsHistoryService(IOperationsHistoryData OperationsHistoryData, IAccountService accountService)
    {
        _OperationsHistoryData = OperationsHistoryData;
        _accountService = accountService;
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutoMapperProfile>();
        });
        _mapper = config.CreateMapper();
    }
    public async Task<List<OperationDTO>> GetOperationsHistotyListAsync(Guid accountID, int pageNumber, int numberOfRecords)
    {
        List<Operation> operationsList = await _OperationsHistoryData.GetOperationsHistotyListAsync(accountID, pageNumber, numberOfRecords);
        List<OperationDTO> operationsListDTO = operationsList.ConvertAll(operation => _mapper.Map<OperationDTO>(operation));
        return operationsListDTO;
    }

    public async Task<CustomerInfoDTO> GetAccountInfoAsync(Guid accountID)
    {
        return await _accountService.GetCustomerInfoAsync(accountID);
    }
}
