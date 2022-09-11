using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Service.DTO
{
    public class OperationsHistoryDTO
    {
        public bool CreditOrDebit { get; set; }
        public Guid AccountID { get; set; }
        public int Amount { get; set; }
        public int Balance { get; set; }
        public DateTime date { get; set; }
    }
}
