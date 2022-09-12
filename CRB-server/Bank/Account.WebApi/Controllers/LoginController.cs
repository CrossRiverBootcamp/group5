using Account.Service;
using Account.Service.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Account.WebApi.Controllers
{
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
            if(accountId == Guid.Empty)
                return Unauthorized("The email or password are worng, please try again");
            return Ok(accountId);
        }

        // GET api/<LoginController>
        [HttpGet("GetCustomerInfoAsync/{accountId}")]
        public async Task<ActionResult> GetCustomerInfoAsync(Guid accountId)
        {
            CustomerInfoDTO customerInfoDTO = await _loginService.GetCustomerInfoAsync(accountId);
            if (customerInfoDTO == null)
                return Unauthorized("the account id is unKnown");
            return Ok(customerInfoDTO);
        }

    }
}
