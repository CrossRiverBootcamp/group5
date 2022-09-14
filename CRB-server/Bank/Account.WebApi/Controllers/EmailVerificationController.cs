using Account.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Account.Service.DTO;

namespace Account.WebApi.Controllers
{
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
        public async Task<ActionResult> SendVerificationCode(string email)
        {
            if (!await _accountService.CreateVerificationCode(email))
                return BadRequest();
            return Ok();
        }
    }
}
