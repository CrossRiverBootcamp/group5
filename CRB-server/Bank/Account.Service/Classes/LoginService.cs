
namespace Account.Service.Classes;

public class LoginService : ILoginService
{
    private readonly ILoginData _loginData;
    private readonly IMapper _mapper;

    public LoginService(ILoginData loginData, IMapper mapper)
    {
        _loginData = loginData;
        _mapper = mapper;
    }

    public Task<Guid> Login(LoginDTO loginDTO)
    {
        Task<Guid> AccountId = _loginData.GetAccountIdAsync(loginDTO.Email, loginDTO.Password);
        return AccountId;
    }

    public async Task<CustomerInfoDTO> GetCustomerInfoAsync(Guid accountId)
    {

        CustomerInfoModel customerInfoModel = await _loginData.GetCustomerInfoAsync(accountId);
        if (customerInfoModel == null)
            return null;
        CustomerInfoDTO customerInfoDTO = _mapper.Map<CustomerInfoDTO>(customerInfoModel);
        return customerInfoDTO;
    }


}
