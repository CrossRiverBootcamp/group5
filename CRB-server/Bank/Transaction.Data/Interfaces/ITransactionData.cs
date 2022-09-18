

namespace Transaction.Data.Interfaces;

public interface ITransactionData
{
    Task<Guid> AddTransactionAsync(Entities.Transaction transaction);
    Task<bool> UpdateTransactionAsync(Guid transactionId, eStatus status, string failureReason);
}
