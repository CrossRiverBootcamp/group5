
namespace Account.Data.Interfaces;

public interface IAccountData
{
    Task<bool> CreateAccountAsync(Entities.Account account, Customer customer);
    Task<bool> CreateEmailVerification(EmailVerification emailVerification);
    Task<bool> IsEmailExistAsync(string email);
    Task<bool> ValidVerificationCode(string email, int code);
    Task<bool> DoBothAccountsExist(Guid fromAccountId, Guid toAccountId);
    Task<bool> IsBalanceGreater(Guid accountId, int amount);
    Task<bool> TransactionBetweenAccountsAndAddOperationAsync(Guid fromAccountId, Guid toAccountId, int amount, Operation operationFromAccount, Operation operationToAccount);
    int GetBalanceByAccountIdAsync(Guid accountId);
    Task<Entities.Account> GetAccountInfoAsync(Guid accountId);

}
