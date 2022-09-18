

namespace Account.Data.Entities;

public class Operation
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [Required]
    public Guid AccountId { get; set; }
    [Required]
    public Guid TransactionId  { get; set; }
    [Required]
    public bool DebitOrCredit { get; set; }
    [Required]
    public int TransactionAmount { get; set; }
    [Required]
    public int Balance { get; set; }
    [Required]
    public DateTime OperationTime { get; set; }

}
