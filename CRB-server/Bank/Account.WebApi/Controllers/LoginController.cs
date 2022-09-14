using Account.Service.DTO;
using Account.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Text;
using System.Net;

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
            //string to = "rivi05832@gmail.com"; //To address    
            //string from = "crbcrproject@gmail.com"; //From address    
            //MailMessage message = new MailMessage(from, to);

            //string mailbody = "In this article you will learn how to send a email using Asp.Net & C#";
            //message.Subject = "Sending Email Using Asp.Net & C#";
            //message.Body = mailbody;
            //message.BodyEncoding = Encoding.UTF8;
            //message.IsBodyHtml = true;
            //SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            //System.Net.NetworkCredential basicCredential1 = new
            //System.Net.NetworkCredential(from, "exmgsvntnurnimcy");
            //client.EnableSsl = true;
            //client.UseDefaultCredentials = false;
            //client.Credentials = basicCredential1;
            //try
            //{
            //    client.Send(message);
            //}

            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            var fromAddress = new MailAddress("crbcrproject@gmail.com", "From crb");
            var toAddress = new MailAddress("shchana63@gmail.com", "To Chana");
            const string fromPassword = "exmgsvntnurnimcy";
            const string subject = "test";
            const string body = "Hey now!!";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            //try
            //{
            //    smtp.Send(message);
            //}

            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            {
                smtp.Send(message);
            }
            Guid accountId = await _loginService.Login(loginDTO);
            if (accountId == Guid.Empty)
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
