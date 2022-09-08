using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Data.Entities
{
    public class Transaction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public Guid FromAccountID { get; set; }
        [Required]
        public Guid ToAccountID { get; set; }
        [Required]
        [Range(1,1000000)]
        public int Amount { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Status { get; set; }// processing -> success / fail
        public string? FailureReason { get; set; }

    }
}
