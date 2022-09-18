
namespace Account.Service.Interfaces;

public interface ILoginService
{
    Task<Guid> Login(LoginDTO loginDTO);
    Task<CustomerInfoDTO> GetCustomerInfoAsync(Guid accountId);
}