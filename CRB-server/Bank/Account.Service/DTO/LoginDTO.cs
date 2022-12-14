
namespace Account.Service.DTO;

public class LoginDTO
{
    [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
    [Required(ErrorMessage = "The Email field is required.")]
    public string Email { get; set; }

    [MaxLength(16)]
    [MinLength(8)]
    [Required(ErrorMessage = "The Password field is required.")]
    public string Password { get; set; }
}
