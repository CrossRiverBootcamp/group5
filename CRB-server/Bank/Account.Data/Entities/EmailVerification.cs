using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Data.Entities
{
    public class EmailVerification
    {
        [Key]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Range(1000,9999)]
        public int VerificationCode { get; set; }
        [Required]
        public TimeOnly ExpirationTime { get; set; }
    }
}
