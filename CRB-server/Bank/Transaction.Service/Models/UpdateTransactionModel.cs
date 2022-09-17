using NSB.Messages;


namespace Transaction.Service.Models;

public class UpdateTransactionModel
{
    public Guid TransactionId { get; set; }
    public eStatus Status { get; set; }
    public string? FailureReason { get; set; }
}
