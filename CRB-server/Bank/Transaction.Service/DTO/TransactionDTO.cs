using System.ComponentModel.DataAnnotations;


namespace Transaction.Service.DTO;

public class TransactionDTO
{
    [Required]
    public Guid FromAccountID { get; set; }
    [Required]
    public Guid ToAccountID { get; set; }
    [Required]
    [Range(1, 1000000)]
    public int Amount { get; set; }
}
