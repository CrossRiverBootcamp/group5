
namespace Account.Service.DTO;

public class CustomerDTO
{
    [MaxLength(50)]
    [MinLength(2)]
    [Required(ErrorMessage = "The FirstName field is required.")]
    public string FirstName { get; set; }

    [MaxLength(50)]
    [MinLength(2)]
    [Required(ErrorMessage = "The LastName field is required.")]
    public string LastName { get; set; }

    [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
    [Required(ErrorMessage = "The Email field is required.")]
    public string Email { get; set; }

    [MaxLength(16)]
    [MinLength(8)]
    [Required(ErrorMessage = "The Password field is required.")]
    public string Password { get; set; }

    [Range(1000,9999)]
    [Required(ErrorMessage = "The verification code field is required.")]
    public int VerificationCode { get; set; }
}
