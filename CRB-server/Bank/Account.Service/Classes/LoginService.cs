
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

    public async Task<Guid> Login(LoginDTO loginDTO)
    {
        return await _loginData.GetAccountIdAsync(loginDTO.Email, loginDTO.Password);
    }

    


}
