using Account.Service;
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
    public class TransactHandler  //: IHandleMessages<TransactionAdded>
    {
        static ILog log = LogManager.GetLogger<TransactHandler>();

        private readonly IAccountService _accountService;
        public TransactHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task Handle(MakeTransfer message, IMessageHandlerContext context)
        {
            Transfered transfered = new Transfered();

            if (await _accountService.DoesAccountExist(message.FromAccountID)
                && await _accountService.DoesAccountExist(message.ToAccountID))
            {
                log.Info($"Successfully transferd from account: {message.FromAccountID} to account: {message.ToAccountID}");
                //transfered.TransactionId = message.
                transfered.Status = "success";
            }
            else
            {

            }



           

            await context.Publish(transfered);

        }
        
    }
}
