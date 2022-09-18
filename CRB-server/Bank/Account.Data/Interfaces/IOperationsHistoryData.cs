
namespace Account.Data.Interfaces;

public interface IOperationsHistoryData
{
    Task<List<Operation>> GetOperationsHistotyListAsync(Guid accountID, int pageNumber, int numberOfRecords);
}