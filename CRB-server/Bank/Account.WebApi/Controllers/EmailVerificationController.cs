
namespace Account.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailVerificationController : ControllerBase
{
    private readonly IAccountService _accountService;

    public EmailVerificationController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("SendVerificationCode")]
    public async Task<ActionResult> SendVerificationCodeAsync([FromBody]string email)
    {
        return !await _accountService.CreateVerificationCodeAsync(email) ? BadRequest(false) : Ok(true);
    }
}
