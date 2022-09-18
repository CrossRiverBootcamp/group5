

namespace Transaction.Data.Entities;

public class Transaction
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [Required]
    public Guid FromAccountID { get; set; }
    [Required]
    public Guid ToAccountID { get; set; }
    [Required]
    [Range(1,1000000)]
    public int Amount { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public eStatus Status { get; set; }
    public string? FailureReason { get; set; }
}
