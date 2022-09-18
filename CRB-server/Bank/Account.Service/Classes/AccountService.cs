
namespace Account.Service.Classes;

public class AccountService : IAccountService
{
    static ILog log = LogManager.GetLogger<AccountService>();
    private readonly IAccountData _accountData;
    private readonly IMapper _mapper;

    public AccountService(IAccountData accountData)
    {
        _accountData = accountData;
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutoMapperProfile>();
        });
        _mapper = config.CreateMapper();

    }

    public async Task<bool> CreateVerificationCode(string email)
    {
        if (await _accountData.IsEmailExistAsync(email))
            return false;
        int code = new Random().Next(1000, 10000);

        EmailVerification emailVerification = new EmailVerification()
        {
            Email = email,
            VerificationCode = code,
            ExpirationTime = DateTime.Now.AddMinutes(30)
        };
        if (await _accountData.CreateEmailVerification(emailVerification))
        {
            var fromAddress = new MailAddress("crbcrproject@gmail.com", "CRB C&R");
            var toAddress = new MailAddress(email);
            const string fromPassword = "nbitifwvunfkzyoa";
            const string subject = "Verification code";
            string body = "Hi We received a request to create a bank account for you. " +
                " Your verification code is: " + code
                + ".  -  this verification code will expire in 30 minutes";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 5000,
                UseDefaultCredentials = false
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
            return true;
        }
        return false;//?
    }

    public async Task<bool> CreateAccountAsync(CustomerDTO customerDTO)
    {
        bool isValid = await _accountData.ValidVerificationCode(customerDTO.Email, customerDTO.VerificationCode);
        if (!isValid)
            return false;
        Customer customer = _mapper.Map<Customer>(customerDTO);
        Data.Entities.Account account = new Data.Entities.Account()
        {
            CustomerId = customer.Id,
            OpenDate = DateTime.UtcNow,
            Balance = 1000
        };
        return await _accountData.CreateAccountAsync(account, customer);       
    }
    public async Task<AccountInfoDTO> GetAccountInfoAsync(Guid accountId)
    {
        Data.Entities.Account account =  await _accountData.GetAccountInfoAsync(accountId);
        if (account == null)
            return null;
        AccountInfoDTO accountInfoDTO = _mapper.Map<AccountInfoDTO>(account);
        return accountInfoDTO;
    }
    public async Task<Transfered> CheckAndTransfer_AddOperations(MakeTransfer message)
    {
        Transfered transfered = new Transfered();
        transfered.TransactionId = message.TransactionId;
        if (message.FromAccountID == message.ToAccountID)
        {
            transfered.Status = eStatus.failure;
            transfered.FailureReason = "can't transfer from and to the same account";
        }
        bool bothExist = await _accountData.DoBothAccountsExist(message.FromAccountID, message.ToAccountID);
        transfered.FailureReason = bothExist == false ? "one or more of the accounts number do not exist." : "";
        if(!bothExist)
            transfered.Status = eStatus.failure;
        bool isGreater = await _accountData.IsBalanceGreater(message.FromAccountID, message.Amount);
        transfered.FailureReason = isGreater == false ? "The amount to be transferred is greater than the 'from' account balance." : "";
        if (!isGreater)
            transfered.Status = eStatus.failure;
        if (transfered.Status != eStatus.failure)
        {
            Operation operationFromAccount = createOperations(message);
            Operation operationToAccount = createOperations(message);
            bool transferedAndOperatedInHistory = await _accountData.
                TransactionBetweenAccountsAndAddOperationAsync(message.FromAccountID, message.ToAccountID, message.Amount, operationFromAccount, operationToAccount);
            transfered.FailureReason = transferedAndOperatedInHistory == false ? "The amount to be transferred is greater than the 'from' account balance." : "";
            transfered.Status = transferedAndOperatedInHistory == false ? eStatus.failure : eStatus.success;
        }
        return transfered;
    }

    private Operation createOperations(MakeTransfer makeTransfer)
    {
        //add model?
        Operation operation = _mapper.Map<Operation>(makeTransfer);
        operation.AccountId = makeTransfer.FromAccountID;
        operation.Debit_Credit = false;
        operation.OperationTime = DateTime.UtcNow;
        operation.Balance = _accountData.GetBalanceByAccountIdAsync(operation.AccountId);
        return operation;
    }
}
