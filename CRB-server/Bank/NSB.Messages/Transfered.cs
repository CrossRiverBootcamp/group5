using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB.Messages
{
    public class Transfered:IEvent
    {
        public Guid TransactionId { get; set; }
        public eStatus Status { get; set; }
        public string? FailureReason { get; set; }
    }
}
