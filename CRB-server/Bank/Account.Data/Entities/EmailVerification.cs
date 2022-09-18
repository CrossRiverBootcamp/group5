
namespace Account.Data.Entities;

[Index("Email", IsUnique = true)]
public class EmailVerification
{
    [Key]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [Range(1000,9999)]
    public int VerificationCode { get; set; }
    [Required]
    public DateTime ExpirationTime { get; set; }
}
