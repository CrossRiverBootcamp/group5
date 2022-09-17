using Account.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;


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
    public async Task<ActionResult> SendVerificationCode([FromBody]string email)
    {
        return !await _accountService.CreateVerificationCode(email) ? BadRequest(false): Ok(true);
    }
}
