
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


    // POST api/<AccountController>
    [HttpPost("AddCustomer")]
    public async Task<ActionResult> AddCustomerAsync([FromBody] CustomerDTO customerDTO)
    {
        string res = await _accountService.AddCustomerAsync(customerDTO);
        return res =="" ? Ok(res) : BadRequest(res);
    }

    
}
