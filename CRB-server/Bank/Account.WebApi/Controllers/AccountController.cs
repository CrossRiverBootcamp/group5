
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Account.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("CreateAccount")]
    public async Task<ActionResult> CreateAccountAsync([FromBody] CustomerDTO customerDTO)
    {
        bool res = await _accountService.CreateAccountAsync(customerDTO);
        return res? Ok(res) : BadRequest("An error occurred, are you sure this is the correct code and it didn't expire yet?");
    }

    [HttpGet("GetAccountInfo/{accountId}")]
    public async Task<ActionResult> GetAccountInfoAsync(Guid accountId)
    {
        AccountInfoDTO accountInfoDTO = await _accountService.GetAccountInfoAsync(accountId);
        return accountInfoDTO == null ? Unauthorized("the account id is unKnown") : Ok(accountInfoDTO);
    }
}
