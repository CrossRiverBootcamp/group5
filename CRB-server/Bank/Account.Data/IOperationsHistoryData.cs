using Account.Data.Entities;

namespace Account.Data
{
    public interface IOperationsHistoryData
    {
        Task<List<Operation>> GetOperationsHistoty(Guid AccountID);
    }
}