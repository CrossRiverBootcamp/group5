using Account.Service;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.NSB
{
    public class TransactHandler
    {
        static ILog log = LogManager.GetLogger<TransactHandler>();

        private readonly IAccountService _accountService;
        public TransactHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task Handle(MakeTransfer message, IMessageHandlerContext context)
        {

        }

    }
}
