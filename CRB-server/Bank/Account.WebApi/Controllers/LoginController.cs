

namespace Account.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    // POST api/<LoginController>
    [HttpPost("LoginAndGetAccountId")]
    public async Task<ActionResult> LoginAndGetAccountId([FromBody] LoginDTO loginDTO)
    {
        Guid accountId = await _loginService.Login(loginDTO);
        return accountId == Guid.Empty ? Unauthorized("The email or password are worng, please try again"): Ok(accountId);  
    }


    // GET api/<LoginController>
    [HttpGet("GetCustomerInfoAsync/{accountId}")]
    public async Task<ActionResult> GetCustomerInfoAsync(Guid accountId)
    {
        CustomerInfoDTO customerInfoDTO = await _loginService.GetCustomerInfoAsync(accountId);
        return customerInfoDTO == null ? Unauthorized("the account id is unKnown") : Ok(customerInfoDTO);             
    }

}
