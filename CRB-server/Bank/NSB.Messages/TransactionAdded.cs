

namespace NSB.Messages;

public class TransactionAdded : IEvent
{
    public Guid TransactionId { get; set; }
   
    public Guid FromAccountID { get; set; }
   
    public Guid ToAccountID { get; set; }
    
    [Range(1, 1000000)]
    public int Amount { get; set; }
}
