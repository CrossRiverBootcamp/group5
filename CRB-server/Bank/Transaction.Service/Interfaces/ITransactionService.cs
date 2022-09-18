
namespace Transaction.Service.Interfaces;

public interface ITransactionService
{
    Task<bool> AddTransactionAsync(TransactionDTO transactionDTO, IMessageSession messageSession);
    //Task UpdateTransactionAsync(UpdateTransactionModel updateTransactionModel);
}
