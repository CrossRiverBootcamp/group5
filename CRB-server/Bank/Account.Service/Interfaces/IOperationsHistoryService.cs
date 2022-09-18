
namespace Account.Service.Interfaces;

public interface IOperationsHistoryService
{
    Task<List<OperationDTO>> GetOperationsHistotyListAsync(Guid accountID, int pageNumber, int numberOfRecords);
    Task<CustomerInfoDTO> GetAccountInfoAsync(Guid accountID);
}