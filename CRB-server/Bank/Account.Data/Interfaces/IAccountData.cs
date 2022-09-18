
namespace Account.Data.Interfaces;

public interface IAccountData
{
    Task<bool> CreateAccountAsync(Entities.Account account);
    Task<bool> AddEmailVerificationAsync(EmailVerification emailVerification);
    Task<bool> IsEmailExistAsync(string email);
    Task<bool> ValidVerificationCodeAsync(string email, int code);
    Task<bool> DoBothAccountsExistAsync(Guid fromAccountId, Guid toAccountId);
    Task<bool> IsBalanceGreaterAsync(Guid accountId, int amount);
    Task<bool> TransactionBetweenAccountsAndAddOperationAsync(Operation operationFromAccount, Operation operationToAccount);
    Task<int> GetBalanceByAccountIdAsync(Guid accountId);
    Task<Entities.Account> GetAccountInfoAsync(Guid accountId);
    Task<bool> CreateCustomerAsync(Customer customer);

}
