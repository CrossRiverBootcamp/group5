
namespace Account.Service.Interfaces;

public interface IAccountService
{
    Task<bool> CreateVerificationCodeAsync(string email);
    Task<bool> CreateAccountAsync(CustomerDTO customerDTO);
    Task<Transfered> CheckTransferAddOperationsAsync(MakeTransfer message);
    Task<AccountInfoDTO> GetAccountInfoAsync(Guid accountId);
    Task<CustomerInfoDTO> GetCustomerInfoAsync(Guid accountId);


}
