
namespace Account.Data.Interfaces;

public interface IAccountData
{
    Task<bool> CreateAccountAsync(Entities.Account account, Customer customer);
    Task<bool> AddEmailVerificationAsync(EmailVerification emailVerification);
    Task<bool> IsEmailExistAsync(string email);
    Task<bool> ValidVerificationCode(string email, int code);
    Task<bool> DoBothAccountsExist(Guid fromAccountId, Guid toAccountId);
    Task<bool> IsBalanceGreater(Guid accountId, int amount);
    Task<bool> TransactionBetweenAccountsAndAddOperationAsync(Operation operationFromAccount, Operation operationToAccount);
    Task<int> GetBalanceByAccountIdAsync(Guid accountId);
    Task<Entities.Account> GetAccountInfoAsync(Guid accountId);

}
