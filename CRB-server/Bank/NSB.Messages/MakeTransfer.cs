using NServiceBus;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB.Messages
{
    public class MakeTransfer:IEvent
    {
        public Guid FromAccountID { get; set; }

        public Guid ToAccountID { get; set; }

        [Range(1, 1000000)]
        public int Amount { get; set; }
    }
}
