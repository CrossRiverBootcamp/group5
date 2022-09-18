
namespace Account.Service.Interfaces;

public interface IAccountService
{
    Task<bool> CreateVerificationCodeAsync(string email);
    Task<bool> CreateAccountAsync(CustomerDTO customerDTO);
    Task<Transferred> CheckTransferAddOperationsAsync(MakeTransfer message);
    Task<AccountInfoDTO> GetAccountInfoAsync(Guid accountId);
    Task<CustomerInfoDTO> GetCustomerInfoAsync(Guid accountId);


}
