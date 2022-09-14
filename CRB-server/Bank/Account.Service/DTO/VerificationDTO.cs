using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Service.DTO
{
    public class VerificationDTO
    {
        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
        [Required(ErrorMessage = "The Email field is required.")]
        public string Email { get; set; }

        [Range(1000, 9999)]
        [Required(ErrorMessage = "The verification code field is required.")]
        public int VerificationCode { get; set; }
    }
}
