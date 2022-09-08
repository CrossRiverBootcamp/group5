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
            Transfered transfered = new Transfered();
            if (message.FromAccountID == message.ToAccountID)
            {
                log.Info($"failed to transfer transferd from account: {message.FromAccountID} to account: {message.ToAccountID}");
                transfered.TransactionId = message.TransactionId;
                transfered.Status = "fail";
                transfered.FailureReason = "can't transfer from and to the same account";
            }
            else if (!await _accountService.DoesAccountExist(message.FromAccountID)
                || !await _accountService.DoesAccountExist(message.ToAccountID))
            {
                log.Info($"failed to transfer from account: {message.FromAccountID} to account: {message.ToAccountID}");
                transfered.TransactionId = message.TransactionId;
                transfered.Status = "fail";
                transfered.FailureReason = "one or more of the accounts number do not exist";
            }
            else if (!await _accountService.IsBalanceGreater(message.FromAccountID, message.Amount))
            {
                log.Info($"failed to transfer from account: {message.FromAccountID} to account: {message.ToAccountID}");
                transfered.TransactionId = message.TransactionId;
                transfered.Status = "fail";
                transfered.FailureReason = "The amount to be transferred is greater than the 'from' account balance";
            }
            else
            {
                if (await _accountService.TransactionBetweenAccountsAsync(message.FromAccountID, message.ToAccountID, message.Amount))
                {
                    log.Info($"Successfully transfered from account: {message.FromAccountID} to account: {message.ToAccountID}");
                    transfered.TransactionId = message.TransactionId;
                    transfered.Status = "success";
                }
                else
                {
                    log.Info($"failed to transfer from account: {message.FromAccountID} to account: {message.ToAccountID}");
                    transfered.TransactionId = message.TransactionId;
                    transfered.Status = "fail";
                    transfered.FailureReason = "An error occurred while updating the data in the DB";
                }
            }

            await context.Publish(transfered);

        }

    }
}
