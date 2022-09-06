using Account.Service.DTO;

namespace Account.Service
{
    public interface ILoginService
    {
        Task<Guid> Login(LoginDTO loginDTO);
        Task<CustomerInfoDTO> GetCustomerInfoAsync(Guid accountId);
    }
}