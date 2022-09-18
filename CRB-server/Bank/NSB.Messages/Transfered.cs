
namespace NSB.Messages;

public class Transfered:IEvent
{
    public Guid TransactionId { get; set; }
    public eStatus Status { get; set; }
    public string? FailureReason { get; set; }
}
