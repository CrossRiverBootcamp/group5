

namespace Account.Data.Entities;

public class Account
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [Required]
    public Guid CustomerId { get; set; }
    [ForeignKey("CustomerId")]
    public virtual Customer Customer { get; set; }
    [Required]
    public DateTime OpenDate { get; set; }
    [Required]
    public int Balance { get; set; }
}
