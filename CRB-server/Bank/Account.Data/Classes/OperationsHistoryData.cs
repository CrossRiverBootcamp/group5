
namespace Account.Data.Classes;

public class OperationsHistoryData : IOperationsHistoryData
{

    private readonly IDbContextFactory<AccountDbContext> _factory;
    public OperationsHistoryData(IDbContextFactory<AccountDbContext> factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    public async Task<List<Operation>> GetOperationsHistotyListAsync(Guid accountID, int pageNumber, int numberOfRecords)
    {
        using var context = _factory.CreateDbContext();
       
        var operationList =
            await  (from o1 in context.Operations
            join o2 in context.Operations 
            on o1.TransactionId equals o2.TransactionId 
            where o1.AccountId == accountID
            select(new Operation
                    {   Id = o1.Id,
                        AccountId = o2.AccountId,
                        TransactionId= o1.TransactionId ,
                        DebitOrCredit= o1.DebitOrCredit,
                        TransactionAmount = o1.TransactionAmount,
                        Balance = o1.Balance,
                        OperationTime = o1.OperationTime
                    })
                 ).Where( operation => operation.AccountId != accountID).OrderByDescending(o=>o.OperationTime)
                 .Skip(numberOfRecords * (pageNumber - 1)).Take(numberOfRecords).ToListAsync();
        return operationList;
    }
}
