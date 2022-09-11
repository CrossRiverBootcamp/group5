using Account.Service.DTO;

namespace Account.Service
{
    public interface IOperationsHistoryService
    {
        Task<List<OperationDTO>> GetOperationsHistotyListAsync(Guid AccountID);
    }
}