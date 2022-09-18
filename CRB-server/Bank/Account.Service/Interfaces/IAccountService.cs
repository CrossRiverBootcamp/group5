
namespace Account.Service.Interfaces;

public interface IAccountService
{
    Task<bool> CreateVerificationCode(string email);
    Task<string> AddCustomerAsync(CustomerDTO customerDTO);
    Task<Transfered> CheckAndTransfer_AddOperations(MakeTransfer message);
}
