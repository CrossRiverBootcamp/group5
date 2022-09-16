using Account.Service.Interfaces;
using NSB.Messages;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.NSB
{
    public class TransactHandler : IHandleMessages<MakeTransfer>
    {
        static ILog log = LogManager.GetLogger<TransactHandler>();

        private readonly IAccountService _accountService;
        public TransactHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task Handle(MakeTransfer message, IMessageHandlerContext context)
        {
            Transfered transfered = await _accountService.CheckAndTransfer_AddOperations(message);
            log.Info($"{transfered.Status} ! the transfer from account: {message.FromAccountID} to account: {message.ToAccountID}");
            await context.Publish(transfered);
        }

    }
}
