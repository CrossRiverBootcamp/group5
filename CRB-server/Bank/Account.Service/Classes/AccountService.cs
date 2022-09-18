
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

    public async Task<bool> CreateVerificationCodeAsync(string email)
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

        if (await _accountData.AddEmailVerificationAsync(emailVerification))
        {
            var fromAddress = new MailAddress("crbcr96@gmail.com", "CRB C&R");
            var toAddress = new MailAddress(email);
            const string fromPassword = "udkwgkwxcrxarsdi";
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
        return false;
    }

    public async Task<bool> CreateAccountAsync(CustomerDTO customerDTO)
    {
        bool isValid = await _accountData.ValidVerificationCodeAsync(customerDTO.Email, customerDTO.VerificationCode);
        if (!isValid)
            return false;
        Customer customer = _mapper.Map<Customer>(customerDTO);
        Data.Entities.Account account = new Data.Entities.Account()
        {
            OpenDate = DateTime.UtcNow,
            Balance = 1000
        };
        return await _accountData.CreateAccountAsync(account, customer);  
    }

    public async Task<CustomerInfoDTO> GetCustomerInfoAsync(Guid accountId)
    {
        Data.Entities.Account account = await _accountData.GetAccountInfoAsync(accountId);
        if (account == null)
            return null;
        CustomerInfoDTO customerInfoDTO = _mapper.Map<CustomerInfoDTO>(account);
        return customerInfoDTO;
    }

    public async Task<AccountInfoDTO> GetAccountInfoAsync(Guid accountId)
    {
        Data.Entities.Account account =  await _accountData.GetAccountInfoAsync(accountId);
        if (account == null)
            return null;
        AccountInfoDTO accountInfoDTO = _mapper.Map<AccountInfoDTO>(account);
        return accountInfoDTO;
    }

    public async Task<Transferred> CheckTransferAddOperationsAsync(MakeTransfer message)
    {
        Transferred transferred = new Transferred();
        transferred.TransactionId = message.TransactionId;
        if (message.FromAccountID == message.ToAccountID)
        {
            transferred.Status = eStatus.failure;
            transferred.FailureReason = "can't transfer from and to the same account";
        }
        else
        {
            bool bothExist = await _accountData.DoBothAccountsExistAsync(message.FromAccountID, message.ToAccountID);
            transferred.FailureReason = bothExist == false ? "one or more of the accounts number do not exist." : null;
            if (!bothExist)
                transferred.Status = eStatus.failure;
            else
            {
                bool isGreater = await _accountData.IsBalanceGreaterAsync(message.FromAccountID, message.Amount);
                transferred.FailureReason = isGreater == false ? "The amount to be transferred is greater than the 'from' account balance." : null;
                if (!isGreater)
                    transferred.Status = eStatus.failure;
            }
        }
   
       
        if (transferred.Status != eStatus.failure)
        {
            bool isTansferred= await makeTransferBetweenAccountsAsync(message);
            
            transferred.FailureReason = isTansferred == false ? "an error occurred in DB" : null;
            transferred.Status = isTansferred == false ? eStatus.failure : eStatus.success;
        }
        return transferred;
    }

    private async Task<bool> makeTransferBetweenAccountsAsync(MakeTransfer makeTransfer)
    {
        Operation operationFromAccount = _mapper.Map<Operation>(makeTransfer);
        operationFromAccount.AccountId = makeTransfer.FromAccountID;
        operationFromAccount.DebitOrCredit = false;
        operationFromAccount.OperationTime = DateTime.UtcNow;
        operationFromAccount.Balance = await _accountData.GetBalanceByAccountIdAsync(operationFromAccount.AccountId) - makeTransfer.Amount;
       
        Operation operationToAccount = _mapper.Map<Operation>(makeTransfer);
        operationToAccount.AccountId = makeTransfer.ToAccountID;
        operationToAccount.DebitOrCredit = true;
        operationToAccount.OperationTime = DateTime.UtcNow;
        operationToAccount.Balance = await _accountData.GetBalanceByAccountIdAsync(operationToAccount.AccountId) + makeTransfer.Amount;
        
        return await _accountData.TransactionBetweenAccountsAndAddOperationAsync(operationFromAccount, operationToAccount);
    }
}
