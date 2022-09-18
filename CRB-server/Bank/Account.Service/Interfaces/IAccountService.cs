
namespace Account.Service.Interfaces;

public interface IAccountService
{
    Task<bool> CreateVerificationCode(string email);
    Task<bool> CreateAccountAsync(CustomerDTO customerDTO);
    Task<Transfered> CheckAndTransfer_AddOperations(MakeTransfer message);
    Task<AccountInfoDTO> GetAccountInfoAsync(Guid accountId);

}
