
namespace NSB.Messages;

public class Transferred:IEvent
{
    public Guid TransactionId { get; set; }
    public eStatus Status { get; set; }
    public string? FailureReason { get; set; }
}
