using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Service.Models
{
    public class UpdateTransactionModel
    {
        public Guid TransactionId { get; set; }
        public string Status { get; set; }
        public string? FailureReason { get; set; }
    }
}
