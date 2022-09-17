using Account.Data.Entities;
using Account.Data.Interfaces;
using Account.Service.DTO;
using Account.Service.Interfaces;
using Account.Services;
using AutoMapper;


namespace Account.Service.Classes;

public class OperationsHistoryService : IOperationsHistoryService
{
    private readonly IOperationsHistoryData _OperationsHistoryData;
    private readonly IMapper _mapper;

    public OperationsHistoryService(IOperationsHistoryData OperationsHistoryData, IMapper mapper)
    {
        _OperationsHistoryData = OperationsHistoryData;
        _mapper = mapper;
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutoMapperProfile>();
        });
        _mapper = config.CreateMapper();
    }
    public async Task<List<OperationDTO>> GetOperationsHistotyListAsync(Guid accountID, int pageNumber, int numberOfRecords)
    {
        List<Operation> operationsList = await _OperationsHistoryData.GetOperationsHistoty(accountID, pageNumber, numberOfRecords);
        List<OperationDTO> operationsListDTO = operationsList.ConvertAll(operation => _mapper.Map<OperationDTO>(operation));
        return operationsListDTO;
    }

    public async Task<AccountInfoDTO> GetAccountInfo(Guid accountID)
    {
        Data.Entities.Account account = await _OperationsHistoryData.GetAccountInfo(accountID);
        AccountInfoDTO accountInfoDTO = _mapper.Map<AccountInfoDTO>(account);
        return accountInfoDTO;
    }

}
