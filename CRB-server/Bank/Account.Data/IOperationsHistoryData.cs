namespace Account.Data
{
    public interface IOperationsHistoryData
    {
        Task<List<Entities.Account>> GetOperationsHistoty(Guid AccountID);
    }
}