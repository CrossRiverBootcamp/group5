using Account.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
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
        public async Task SendVerificationCode(CustomerDTO customerDTO)
        {
            var fromAddress = new MailAddress("crbcrproject@gmail.com", "CRB C&R");
            var toAddress = new MailAddress(customerDTO.Email, customerDTO.FirstName);
            const string fromPassword = "nbitifwvunfkzyoa";

            Random random = new Random();
            int code = random.Next(1000,10000);
            const string subject = "Verification code";
            const string body = "Hi! this isHello CRB, We received a request to create a bank account and use your email address." +
                " Your verification code  from CROSS RIVER BANK is: " ;


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 5000,
                UseDefaultCredentials = false
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        
        }
    }
}
